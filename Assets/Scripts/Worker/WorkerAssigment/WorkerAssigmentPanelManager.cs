using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerAssignmentPanelManager : MonoBehaviour
{
    public GameObject panel; // Panel referansý
    public TMP_Text ihaleAdiText;
    public Button assignButton;

    private IhaleData currentIhale;
    private int currentSlotIndex;
    private PlayerData playerData;

    public TMP_Text goodEngineerCountText;
    public TMP_Text badEngineerCountText;
    public TMP_Text goodWorkerCountText;
    public TMP_Text badWorkerCountText;

    public GameObject[] ihaleTutucularýArray;

    private AssignWorker assignWorker;

    private int ihaleIndex; // Eklenen index deðiþkeni

    void Start()
    {
        assignButton.onClick.AddListener(AssignWorkers);
        panel.SetActive(false); // Baþlangýçta panel kapalý
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
        this.ihaleIndex = ihaleIndex; // Index'i burada atýyoruz
        ihaleAdiText.text = ihaleData.ihaleAdi;
        panel.SetActive(true);
        // Ýþçi sayýsýný UI'da güncelle
        UpdateWorkerCounts();

    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void AssignWorkers()
    {
        Dictionary<WorkerData, int> assignedWorkers = assignWorker.GetAssignedWorkers();

        // Ýþçilerin yeterliliðini kontrol et
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
            ihaleTutucularýArray[ihaleIndex].SetActive(false); // Index'i burada kullanýyoruz
            // Ýþçileri atama iþlemleri
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

            // Ýhaleyi baþlat
            playerData.ihaleCoroutine.StartIhale(currentSlotIndex, currentIhale, assignedWorkers);

            // ÞirketUIManager'a ihale adýný gönderme
            playerData.sirketUIManager.SetIhaleAdi(currentIhale.ihaleAdi, currentSlotIndex);

            // Final completion rate hesapla ve UI güncelle
            float totalWorkerScore = 0f;
            foreach (var workerEntry in assignedWorkers)
            {
                totalWorkerScore += workerEntry.Key.workerScore * workerEntry.Value;
            }

            float finalCompletionRate = currentIhale.gerceklesmeOrani + totalWorkerScore;
            finalCompletionRate = Mathf.Clamp(finalCompletionRate, 0f, 100f);
            playerData.sirketUIManager.UpdateCompletionRateUI(finalCompletionRate, currentSlotIndex);

            // Ýþçi sayýsýný güncelle
            UpdateWorkerCounts();

            assignWorker.ResetCounts();

            // Paneli kapat
            ClosePanel();
        }
        else
        {
            Debug.Log("Yeterli mühendis veya iþçi yok.");
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
