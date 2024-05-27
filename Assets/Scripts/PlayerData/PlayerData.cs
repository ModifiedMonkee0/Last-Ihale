using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float currentMoney;
    public int workerCount;
    public float totalHourlyWage;   // Ýþçilere ödenmesi gereken toplam saatlik ücret

    // Ýþçi türleri için ScriptableObject referanslarý
    public GoodEngineer goodEngineerData;
    public BadEngineer badEngineerData;
    public GoodWorker goodWorkerData;
    public BadWorker badWorkerData;

    // ÞirketUIManager ve IhaleCoroutine referanslarý
    public SirketUIManager sirketUIManager;
    public IhaleCoroutine ihaleCoroutine;

    private void Awake()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("CurrentMoney", currentMoney);
        PlayerPrefs.SetInt("WorkerCount", workerCount);
        PlayerPrefs.SetFloat("TotalHourlyWage", totalHourlyWage);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        currentMoney = PlayerPrefs.GetFloat("CurrentMoney", 1000000f);  // Varsayýlan deðer 1000000
        workerCount = PlayerPrefs.GetInt("WorkerCount", 0);          // Varsayýlan deðer 0
        totalHourlyWage = PlayerPrefs.GetFloat("TotalHourlyWage", 0f); // Varsayýlan deðer 0
    }

    public bool HireWorker(WorkerData worker)
    {
        if (currentMoney >= worker.hiringCost)
        {
            currentMoney -= worker.hiringCost;
            workerCount++;
            totalHourlyWage += worker.hourlyWage;

            // Ýþçi türüne göre sayýyý artýr
            if (worker is GoodEngineer)
            {
                goodEngineerData.count++;
            }
            else if (worker is BadEngineer)
            {
                badEngineerData.count++;
            }
            else if (worker is GoodWorker)
            {
                goodWorkerData.count++;
            }
            else if (worker is BadWorker)
            {
                badWorkerData.count++;
            }

            SaveData();
            return true;
        }
        return false;
    }

    public void PayWorkers()
    {
        if (currentMoney >= totalHourlyWage)
        {
            currentMoney -= totalHourlyWage;
            SaveData();
        }
        else
        {
            Debug.Log("Yeterli para yok, iþçilere ödeme yapýlamadý!");
        }
    }
}
