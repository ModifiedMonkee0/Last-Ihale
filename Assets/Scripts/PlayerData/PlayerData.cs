using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float currentMoney;
    public int workerCount;
    public float totalHourlyWage;   // ���ilere �denmesi gereken toplam saatlik �cret

    // ���i t�rleri i�in ScriptableObject referanslar�
    public GoodEngineer goodEngineerData;
    public BadEngineer badEngineerData;
    public GoodWorker goodWorkerData;
    public BadWorker badWorkerData;

    // �irketUIManager ve IhaleCoroutine referanslar�
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
        currentMoney = PlayerPrefs.GetFloat("CurrentMoney", 1000000f);  // Varsay�lan de�er 1000000
        workerCount = PlayerPrefs.GetInt("WorkerCount", 0);          // Varsay�lan de�er 0
        totalHourlyWage = PlayerPrefs.GetFloat("TotalHourlyWage", 0f); // Varsay�lan de�er 0
    }

    public bool HireWorker(WorkerData worker)
    {
        if (currentMoney >= worker.hiringCost)
        {
            currentMoney -= worker.hiringCost;
            workerCount++;
            totalHourlyWage += worker.hourlyWage;

            // ���i t�r�ne g�re say�y� art�r
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

    public bool SellWorker(WorkerData worker)
    {
        // ���i t�r�ne g�re say�y� azalt ve toplam saatlik �creti g�ncelle
        if (worker is GoodEngineer && goodEngineerData.count > 0)
        {
            goodEngineerData.count--;
        }
        else if (worker is BadEngineer && badEngineerData.count > 0)
        {
            badEngineerData.count--;
        }
        else if (worker is GoodWorker && goodWorkerData.count > 0)
        {
            goodWorkerData.count--;
        }
        else if (worker is BadWorker && badWorkerData.count > 0)
        {
            badWorkerData.count--;
        }
        else
        {
            Debug.Log("Sat�lacak i��i yok: " + worker.workerName);
            return false;
        }

        workerCount--;
        totalHourlyWage -= worker.hourlyWage;
        currentMoney += worker.sat��Fiyat�;

        SaveData();
        return true;
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
            Debug.Log("Yeterli para yok, i��ilere �deme yap�lamad�!");
            float borc = currentMoney - totalHourlyWage;
            Debug.Log("Bor�land�n! Toplam Bor�" + borc);
        }
    }

    // kredi i�lemleri
    public void TakeLoan(float amount, float interestRate, float repaymentTime)
    {
        currentMoney += amount;
        SaveData();
        StartCoroutine(RepayLoan(amount, interestRate, repaymentTime));
    }

    private IEnumerator RepayLoan(float amount, float interestRate, float repaymentTime)
    {
        yield return new WaitForSeconds(repaymentTime);
        float repaymentAmount = amount * (1 + interestRate / 100);
        if (currentMoney >= repaymentAmount)
        {
            currentMoney -= repaymentAmount;
        }
        else
        {
            Debug.Log("Yeterli para yok, kredi geri �denemedi!");
            // Bor� i�lemleri burada eklenebilir
        }
        SaveData();
    }
}
