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

    private AssignWorker assignWorker;

    void Start()
    {
        assignButton.onClick.AddListener(AssignWorkers);
        panel.SetActive(false); // Ba�lang��ta panel kapal�
        assignWorker = FindObjectOfType<AssignWorker>(); // AssignWorker scriptine referans al
    }

    public void OpenPanel(IhaleData ihaleData, int slotIndex, PlayerData player)
    {
        currentIhale = ihaleData;
        currentSlotIndex = slotIndex;
        playerData = player;
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
        int totalAssignedWorkers = 0;
        foreach (var workerCount in assignedWorkers.Values)
        {
            totalAssignedWorkers += workerCount;
        }

        if (totalAssignedWorkers >= currentIhale.gerekliIsciler)
        {
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

            // ���i say�s�n� g�ncelle
            UpdateWorkerCounts();

            assignWorker.ResetCounts();

            // Paneli kapat
            ClosePanel();
        }
        else
        {
            Debug.Log("Yeterli i��i yok.");
        }
    }

    private void UpdateWorkerCounts()
    {
        goodEngineerCountText.text = ("�yi M�hendis: " + playerData.goodEngineerData.count.ToString());
        badEngineerCountText.text = ("K�t� M�hendis:" + playerData.badEngineerData.count.ToString());
        goodWorkerCountText.text = ("�yi i�ci: " + playerData.goodWorkerData.count.ToString());
        badWorkerCountText.text = ("K�t� ��ci: " + playerData.badWorkerData.count.ToString());
    }
}
