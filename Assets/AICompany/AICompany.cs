using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompany : MonoBehaviour
{
    public PlayerData aiPlayerData;
    public IhaleData[] availableIhaleler;
    public float checkInterval = 10f; // AI'nin ihaleleri kontrol etme sýklýðý
    private IhaleUIManager ihaleUIManager;

    void Start()
    {
        ihaleUIManager = FindObjectOfType<IhaleUIManager>();
        StartCoroutine(CheckForIhalesRoutine());
    }

    IEnumerator CheckForIhalesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            CheckForIhales();
        }
    }

    void CheckForIhales()
    {
        if (ihaleUIManager.sirketUIManager == null)
        {
            Debug.LogError("sirketUIManager referansý bulunamadý!");
            return;
        }

        foreach (IhaleData ihale in availableIhaleler)
        {
            if (aiPlayerData.currentMoney >= ihale.ihaleFiyati && aiPlayerData.workerCount >= ihale.gerekliIsciler)
            {
                Debug.Log("Ýþcileri kontrol etti");
                // AI'nin ihale satýn almasý
                int availableSlot = ihaleUIManager.sirketUIManager.GetFirstAvailableSlot();
                Debug.Log("Satýn Aldý");
                if (availableSlot != -1)
                {
                    // Ýþçileri otomatik olarak ata
                    Dictionary<WorkerData, int> assignedWorkers = AssignWorkersForIhale(ihale);

                    if (assignedWorkers != null)
                    {
                        aiPlayerData.currentMoney -= ihale.ihaleFiyati;
                        ihaleUIManager.sirketUIManager.SetIhaleAdi(ihale.ihaleAdi, availableSlot);
                        ihaleUIManager.ihaleCoroutine.StartIhale(availableSlot, ihale, assignedWorkers);
                    }
                }
            }
        }
    }

    Dictionary<WorkerData, int> AssignWorkersForIhale(IhaleData ihale)
    {
        Dictionary<WorkerData, int> assignedWorkers = new Dictionary<WorkerData, int>();
        int remainingWorkers = ihale.gerekliIsciler;

        if (aiPlayerData.goodEngineerData.count > 0)
        {
            int count = Mathf.Min(aiPlayerData.goodEngineerData.count, remainingWorkers);
            assignedWorkers[aiPlayerData.goodEngineerData] = count;
            aiPlayerData.goodEngineerData.count -= count;
            remainingWorkers -= count;
        }

        if (remainingWorkers > 0 && aiPlayerData.badEngineerData.count > 0)
        {
            int count = Mathf.Min(aiPlayerData.badEngineerData.count, remainingWorkers);
            assignedWorkers[aiPlayerData.badEngineerData] = count;
            aiPlayerData.badEngineerData.count -= count;
            remainingWorkers -= count;
        }

        if (remainingWorkers > 0 && aiPlayerData.goodWorkerData.count > 0)
        {
            int count = Mathf.Min(aiPlayerData.goodWorkerData.count, remainingWorkers);
            assignedWorkers[aiPlayerData.goodWorkerData] = count;
            aiPlayerData.goodWorkerData.count -= count;
            remainingWorkers -= count;
        }

        if (remainingWorkers > 0 && aiPlayerData.badWorkerData.count > 0)
        {
            int count = Mathf.Min(aiPlayerData.badWorkerData.count, remainingWorkers);
            assignedWorkers[aiPlayerData.badWorkerData] = count;
            aiPlayerData.badWorkerData.count -= count;
            remainingWorkers -= count;
        }

        if (remainingWorkers > 0)
        {
            Debug.Log("AI yeterli iþçi bulamadý.");
            return null;
        }

        return assignedWorkers;
    }
}
