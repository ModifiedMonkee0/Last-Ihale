using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhaleCoroutine : MonoBehaviour
{
    public event Action<float, int> OnTimerTick; // Her saniyede bir �a�r�lacak olay, ihale indeksi ekledik
    public event Action<int> OnIhaleCompleted;  // �hale tamamland���nda �a�r�lacak olay

    // �� ayr� ihale i�in zamanlay�c�lar� tutan diziler
    private float[] remainingTimes = new float[3];
    private Coroutine[] coroutines = new Coroutine[3];
    private List<WorkerData>[] assignedWorkers = new List<WorkerData>[3]; // Atanan i��ileri tutar

    private PlayerData playerData; // PlayerData referans�

    public IEnumerator IhaleSonucuCoroutine(IhaleData ihaleData, WorkerData[] workers, int index)
    {
        float remainingTime = 10f; // Ba�lang��ta kalan s�re 10 saniye
        remainingTimes[index] = remainingTime;
        assignedWorkers[index] = new List<WorkerData>(workers); // ���ileri kaydet

        while (remainingTimes[index] > 0f)
        {
            // Her saniyede bir kalan s�reyi azalt
            remainingTimes[index] -= Time.deltaTime;

            // Olay� tetikle ve kalan s�reyi dinleyenlere aktar
            OnTimerTick?.Invoke(remainingTimes[index], index);

            yield return null; // Bir sonraki frame'i bekle
        }

        // ���ilerin toplam puan�n� hesapla
        float totalWorkerScore = 0f;
        foreach (WorkerData worker in workers)
        {
            totalWorkerScore += worker.workerScore;
        }

        // Ger�ekle�me oran�n� i��i puanlar�yla birle�tir
        float finalCompletionRate = ihaleData.gerceklesmeOrani + totalWorkerScore;
        finalCompletionRate = Mathf.Clamp(finalCompletionRate, 0f, 100f); // 0 ile 100 aras�nda k�s�tla
        Debug.Log("final compation rate=" + finalCompletionRate);
        Debug.Log("total worker score=" + totalWorkerScore);

        // Zaman doldu�unda sonucu belirle
        bool isSuccessful = UnityEngine.Random.value <= finalCompletionRate / 100f;

        if (isSuccessful)
        {
            Debug.Log("�hale iyi sonu�land�.");
            playerData.currentMoney += ihaleData.ihaleFiyati * 1.25f; // currentMoney'yi %125 art�r
            playerData.SaveData();
        }
        else
        {
            Debug.Log("�hale k�t� sonu�land�.");
        }

        // �hale tamamland���nda i��ileri serbest b�rak
        assignedWorkers[index].Clear();

        // �hale tamamland���nda olay tetikle
        OnIhaleCompleted?.Invoke(index);
    }

    public void StartIhale(int index, IhaleData ihaleData, WorkerData[] workers)
    {
        if (coroutines[index] != null)
        {
            StopCoroutine(coroutines[index]);
        }

        // PlayerData referans�n� burada atay�n
        playerData = FindObjectOfType<PlayerData>();

        coroutines[index] = StartCoroutine(IhaleSonucuCoroutine(ihaleData, workers, index));
    }
}
