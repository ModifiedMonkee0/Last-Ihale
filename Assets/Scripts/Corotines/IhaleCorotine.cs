using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhaleCoroutine : MonoBehaviour
{
    public event Action<float, int> OnTimerTick; // Her saniyede bir çaðrýlacak olay, ihale indeksi ekledik
    public event Action<int> OnIhaleCompleted;  // Ýhale tamamlandýðýnda çaðrýlacak olay

    // Üç ayrý ihale için zamanlayýcýlarý tutan diziler
    private float[] remainingTimes = new float[3];
    private Coroutine[] coroutines = new Coroutine[3];
    private Dictionary<WorkerData, int>[] assignedWorkers = new Dictionary<WorkerData, int>[3]; // Atanan iþçileri tutar

    private PlayerData playerData; // PlayerData referansý

    public IEnumerator IhaleSonucuCoroutine(IhaleData ihaleData, Dictionary<WorkerData, int> workers, int index)
    {
        float remainingTime = 10f; // Baþlangýçta kalan süre 10 saniye
        remainingTimes[index] = remainingTime;
        assignedWorkers[index] = new Dictionary<WorkerData, int>(workers); // Ýþçileri kaydet

        while (remainingTimes[index] > 0f)
        {
            // Her saniyede bir kalan süreyi azalt
            remainingTimes[index] -= Time.deltaTime;

            // Olayý tetikle ve kalan süreyi dinleyenlere aktar
            OnTimerTick?.Invoke(remainingTimes[index], index);

            yield return null; // Bir sonraki frame'i bekle
        }

        // Ýþçilerin toplam puanýný hesapla
        float totalWorkerScore = 0f;
        foreach (var workerEntry in workers)
        {
            totalWorkerScore += workerEntry.Key.workerScore * workerEntry.Value;
        }

        // Gerçekleþme oranýný iþçi puanlarýyla birleþtir
        float finalCompletionRate = ihaleData.gerceklesmeOrani + totalWorkerScore;
        finalCompletionRate = Mathf.Clamp(finalCompletionRate, 0f, 100f); // 0 ile 100 arasýnda kýsýtla
        Debug.Log("final compation rate=" + finalCompletionRate);
        Debug.Log("total worker score=" + totalWorkerScore);

        // Zaman dolduðunda sonucu belirle
        bool isSuccessful = UnityEngine.Random.value <= finalCompletionRate / 100f;

        if (isSuccessful)
        {
            Debug.Log("Ýhale iyi sonuçlandý.");
            playerData.currentMoney += ihaleData.ihaleFiyati * 1.25f; // currentMoney'yi %125 artýr
            playerData.SaveData();
        }
        else
        {
            Debug.Log("Ýhale kötü sonuçlandý.");
        }

        // Ýhale tamamlandýðýnda iþçileri geri ekle
        foreach (var workerEntry in workers)
        {
            if (workerEntry.Key is GoodEngineer)
            {
                (workerEntry.Key as GoodEngineer).count += workerEntry.Value;
            }
            else if (workerEntry.Key is BadEngineer)
            {
                (workerEntry.Key as BadEngineer).count += workerEntry.Value;
            }
            else if (workerEntry.Key is GoodWorker)
            {
                (workerEntry.Key as GoodWorker).count += workerEntry.Value;
            }
            else if (workerEntry.Key is BadWorker)
            {
                (workerEntry.Key as BadWorker).count += workerEntry.Value;
            }
        }

        // Ýhale tamamlandýðýnda iþçileri serbest býrak
        assignedWorkers[index].Clear();

        // Ýhale tamamlandýðýnda olay tetikle
        OnIhaleCompleted?.Invoke(index);
    }

    public void StartIhale(int index, IhaleData ihaleData, Dictionary<WorkerData, int> workers)
    {
        if (coroutines[index] != null)
        {
            StopCoroutine(coroutines[index]);
        }

        // PlayerData referansýný burada atayýn
        playerData = FindObjectOfType<PlayerData>();

        coroutines[index] = StartCoroutine(IhaleSonucuCoroutine(ihaleData, workers, index));
    }
}
