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

    private AssignWorker assignWorker;

    void Start()
    {
        assignButton.onClick.AddListener(AssignWorkers);
        panel.SetActive(false); // Baþlangýçta panel kapalý
        assignWorker = FindObjectOfType<AssignWorker>(); // AssignWorker scriptine referans al
    }

    public void OpenPanel(IhaleData ihaleData, int slotIndex, PlayerData player)
    {
        currentIhale = ihaleData;
        currentSlotIndex = slotIndex;
        playerData = player;
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
        int totalAssignedWorkers = 0;
        foreach (var workerCount in assignedWorkers.Values)
        {
            totalAssignedWorkers += workerCount;
        }

        if (totalAssignedWorkers >= currentIhale.gerekliIsciler)
        {
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

            // Ýþçi sayýsýný güncelle
            UpdateWorkerCounts();

            assignWorker.ResetCounts();

            // Paneli kapat
            ClosePanel();
        }
        else
        {
            Debug.Log("Yeterli iþçi yok.");
        }
    }

    private void UpdateWorkerCounts()
    {
        goodEngineerCountText.text = ("Ýyi Mühendis: " + playerData.goodEngineerData.count.ToString());
        badEngineerCountText.text = ("Kötü Mühendis:" + playerData.badEngineerData.count.ToString());
        goodWorkerCountText.text = ("Ýyi iþci: " + playerData.goodWorkerData.count.ToString());
        badWorkerCountText.text = ("Kötü Ýþci: " + playerData.badWorkerData.count.ToString());
    }
}
