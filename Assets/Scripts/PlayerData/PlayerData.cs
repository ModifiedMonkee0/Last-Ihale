using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float currentMoney;
    public int workerCount;
    public float totalHourlyWage;   // Ýþçilere ödenmesi gereken toplam saatlik ücret

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
        currentMoney = PlayerPrefs.GetFloat("CurrentMoney", 1000f);  // Varsayýlan deðer 1000
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
