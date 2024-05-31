using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerAssignmentPanelManager : MonoBehaviour
{
    public GameObject panel; // Panel referans�
    public TMP_Text ihaleAdiText;
    public Button assignButton;

    private IhaleData currentIhale;
    private int currentSlotIndex;
    private PlayerData playerData;

    public TMP_Text goodEngineerCountText;
    public TMP_Text badEngineerCountText;
    public TMP_Text goodWorkerCountText;
    public TMP_Text badWorkerCountText;

    public GameObject[] ihaleTutucular�Array;

    private AssignWorker assignWorker;

    private int ihaleIndex; // Eklenen index de�i�keni

    void Start()
    {
        assignButton.onClick.AddListener(AssignWorkers);
        panel.SetActive(false); // Ba�lang��ta panel kapal�
        assignWorker = FindObjectOfType<AssignWorker>(); // AssignWorker scriptine referans al
    }
    private void Update()
    {
       
    }
    public void OpenPanel(IhaleData ihaleData, int slotIndex, PlayerData player, int ihaleIndex)
    {
        currentIhale = ihaleData;
        currentSlotIndex = slotIndex;
        playerData = player;
        this.ihaleIndex = ihaleIndex; // Index'i burada at�yoruz
        ihaleAdiText.text = ihaleData.ihaleAdi;
        panel.SetActive(true);
        // ���i say�s�n� UI'da g�ncelle
        UpdateWorkerCounts();

    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void AssignWorkers()
    {
        Dictionary<WorkerData, int> assignedWorkers = assignWorker.GetAssignedWorkers();

        // ���ilerin yeterlili�ini kontrol et
        int totalEngineers = 0;
        int totalWorkers = 0;

        foreach (var workerEntry in assignedWorkers)
        {
            if (workerEntry.Key is GoodEngineer || workerEntry.Key is BadEngineer)
            {
                totalEngineers += workerEntry.Value;
            }
            else if (workerEntry.Key is GoodWorker || workerEntry.Key is BadWorker)
            {
                totalWorkers += workerEntry.Value;
            }
        }

        if (totalEngineers >= currentIhale.gerekliMuhendisler && totalWorkers >= currentIhale.gerekliIsciler)
        {
            ihaleTutucular�Array[ihaleIndex].SetActive(false); // Index'i burada kullan�yoruz
            // ���ileri atama i�lemleri
            foreach (var workerEntry in assignedWorkers)
            {
                if (workerEntry.Key is GoodEngineer)
                {
                    (workerEntry.Key as GoodEngineer).count -= workerEntry.Value;
                }
                else if (workerEntry.Key is BadEngineer)
                {
                    (workerEntry.Key as BadEngineer).count -= workerEntry.Value;
                }
                else if (workerEntry.Key is GoodWorker)
                {
                    (workerEntry.Key as GoodWorker).count -= workerEntry.Value;
                }
                else if (workerEntry.Key is BadWorker)
                {
                    (workerEntry.Key as BadWorker).count -= workerEntry.Value;
                }
            }

            

            playerData.currentMoney -= currentIhale.ihaleFiyati;
            playerData.SaveData();

            // �haleyi ba�lat
            playerData.ihaleCoroutine.StartIhale(currentSlotIndex, currentIhale, assignedWorkers);

            // �irketUIManager'a ihale ad�n� g�nderme
            playerData.sirketUIManager.SetIhaleAdi(currentIhale.ihaleAdi, currentSlotIndex);

            // Final completion rate hesapla ve UI g�ncelle
            float totalWorkerScore = 0f;
            foreach (var workerEntry in assignedWorkers)
            {
                totalWorkerScore += workerEntry.Key.workerScore * workerEntry.Value;
            }

            float finalCompletionRate = currentIhale.gerceklesmeOrani + totalWorkerScore;
            finalCompletionRate = Mathf.Clamp(finalCompletionRate, 0f, 100f);
            playerData.sirketUIManager.UpdateCompletionRateUI(finalCompletionRate, currentSlotIndex);

            // ���i say�s�n� g�ncelle
            UpdateWorkerCounts();

            assignWorker.ResetCounts();

            // Paneli kapat
            ClosePanel();
        }
        else
        {
            Debug.Log("Yeterli m�hendis veya i��i yok.");
        }
    }

    public void UpdateWorkerCounts()
    {
        goodEngineerCountText.text = ("Good Engineer: " + playerData.goodEngineerData.count.ToString());
        badEngineerCountText.text = ("Bad Engineer:" + playerData.badEngineerData.count.ToString());
        goodWorkerCountText.text = ("Good Worker: " + playerData.goodWorkerData.count.ToString());
        badWorkerCountText.text = ("Bad Worker: " + playerData.badWorkerData.count.ToString());
    }
}
