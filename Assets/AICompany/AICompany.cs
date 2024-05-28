using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompany : MonoBehaviour
{
    public PlayerData aiPlayerData;
    public IhaleData[] availableIhaleler;
    public float checkInterval = 10f; // AI'nin ihaleleri kontrol etme s�kl���
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
            Debug.LogError("sirketUIManager referans� bulunamad�!");
            return;
        }

        foreach (IhaleData ihale in availableIhaleler)
        {
            if (aiPlayerData.currentMoney >= ihale.ihaleFiyati && aiPlayerData.workerCount >= ihale.gerekliIsciler)
            {
                Debug.Log("��cileri kontrol etti");
                // AI'nin ihale sat�n almas�
                int availableSlot = ihaleUIManager.sirketUIManager.GetFirstAvailableSlot();
                Debug.Log("Sat�n Ald�");
                if (availableSlot != -1)
                {
                    // ���ileri otomatik olarak ata
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
            Debug.Log("AI yeterli i��i bulamad�.");
            return null;
        }

        return assignedWorkers;
    }
}
