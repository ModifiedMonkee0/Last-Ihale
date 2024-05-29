using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompany : MonoBehaviour
{
    public AICompanyData aiCompanyData;
    public List<IhaleData> availableIhaleler;
    public float checkInterval = 10f; // AI'nin ihaleleri kontrol etme s�kl���
    public float workerAdjustmentInterval = 30f; // AI'nin i��i say�s�n� ayarlama s�kl���
    public int workerCost = 5000; // Her i��i i�in maliyet
    public int workerMaintenanceCost = 1000; // Her 30 saniyede her i��i i�in maliyet
    public int maxWorkerAdjustment = 3; // Her seferinde maksimum ayarlanabilir i��i say�s�
    private System.Random random = new System.Random();

    void Start()
    {
        StartCoroutine(CheckForIhalesRoutine());
        StartCoroutine(EvaluateAndPurchaseBestIhaleRoutine());
        StartCoroutine(PayWorkerMaintenanceRoutine());
    }

    IEnumerator CheckForIhalesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            EvaluateAndPurchaseBestIhale();
        }
    }

    IEnumerator EvaluateAndPurchaseBestIhaleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(workerAdjustmentInterval);
            EvaluateAndPurchaseBestIhale();
        }
    }

    IEnumerator PayWorkerMaintenanceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(workerAdjustmentInterval);
            PayWorkerMaintenance();
        }
    }

    void EvaluateAndPurchaseBestIhale()
    {
        IhaleData bestIhale = SelectBestIhale();
        if (bestIhale != null)
        {
            int requiredWorkers = bestIhale.gerekliIsciler;
            int requiredAdditionalWorkers = requiredWorkers - aiCompanyData.totalWorkers;

            if (requiredAdditionalWorkers > 0)
            {
                int turnsToHire = Mathf.CeilToInt((float)requiredAdditionalWorkers / maxWorkerAdjustment);
                float totalAdditionalCost = requiredAdditionalWorkers * workerCost;
                float totalMaintenanceCost = CalculateTotalMaintenanceCost(turnsToHire, requiredWorkers);
                float expectedProfit = CalculateIhaleScore(bestIhale) * (bestIhale.ihaleFiyati * 1.5f);
                float totalCost = totalAdditionalCost + totalMaintenanceCost;
                float expectedNetProfit = expectedProfit - totalCost;

                if (expectedNetProfit > 0 && aiCompanyData.totalMoney >= totalCost)
                {
                    PurchaseIhale(bestIhale);
                }
                else
                {
                    bestIhale = FindAffordableIhale();
                    if (bestIhale != null)
                    {
                        PurchaseIhale(bestIhale);
                    }
                }
            }
            else
            {
                PurchaseIhale(bestIhale);
            }
        }
    }

    IhaleData FindAffordableIhale()
    {
        IhaleData bestAffordableIhale = null;
        float bestScore = float.MinValue;

        foreach (IhaleData ihale in availableIhaleler)
        {
            int requiredWorkers = ihale.gerekliIsciler;
            int requiredAdditionalWorkers = requiredWorkers - aiCompanyData.totalWorkers;
            int turnsToHire = Mathf.CeilToInt((float)requiredAdditionalWorkers / maxWorkerAdjustment);
            float totalAdditionalCost = requiredAdditionalWorkers * workerCost;
            float totalMaintenanceCost = CalculateTotalMaintenanceCost(turnsToHire, requiredWorkers);
            float expectedProfit = CalculateIhaleScore(ihale) * (ihale.ihaleFiyati * 1.5f);
            float totalCost = totalAdditionalCost + totalMaintenanceCost;
            float expectedNetProfit = expectedProfit - totalCost;

            if (expectedNetProfit > 0 && aiCompanyData.totalMoney >= totalCost)
            {
                if (expectedNetProfit > bestScore)
                {
                    bestScore = expectedNetProfit;
                    bestAffordableIhale = ihale;
                }
            }
        }

        return bestAffordableIhale;
    }

    float CalculateTotalMaintenanceCost(int turnsToHire, int requiredWorkers)
    {
        int totalWorkers = aiCompanyData.totalWorkers;
        float totalMaintenanceCost = 0f;

        for (int i = 1; i <= turnsToHire; i++)
        {
            totalWorkers += maxWorkerAdjustment;
            totalMaintenanceCost += totalWorkers * workerMaintenanceCost;
        }

        totalWorkers += requiredWorkers - maxWorkerAdjustment * turnsToHire;
        totalMaintenanceCost += totalWorkers * workerMaintenanceCost;

        return totalMaintenanceCost;
    }

    void PayWorkerMaintenance()
    {
        int totalCost = aiCompanyData.totalWorkers * workerMaintenanceCost;
        aiCompanyData.totalMoney -= totalCost;
        Debug.Log($"AI '{aiCompanyData.companyName}' {aiCompanyData.totalWorkers} i��i i�in {totalCost} bak�m �creti �dedi. Kalan para: {aiCompanyData.totalMoney}");
    }

    IhaleData SelectBestIhale()
    {
        IhaleData bestIhale = null;
        float bestScore = float.MinValue;

        foreach (IhaleData ihale in availableIhaleler)
        {
            float score = CalculateIhaleScore(ihale);
            score = ApplyDifficultyError(score);
            Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' i�in hesaplanan skor: {score}");

            if (score > bestScore)
            {
                bestScore = score;
                bestIhale = ihale;
            }
        }

        return bestIhale;
    }

    float CalculateIhaleScore(IhaleData ihale)
    {
        float successChance = ihale.gerceklesmeOrani / 100f;
        float potentialProfit = ihale.ihaleFiyati * 1.5f;
        int requiredWorkers = ihale.gerekliIsciler;

        // ���i ba��na %5 ba�ar� art���
        successChance += requiredWorkers * 0.05f;

        // Karl�l�k skoru hesaplama (potansiyel kar * ba�ar� �ans�)
        return potentialProfit * successChance - ihale.ihaleFiyati;
    }

    float ApplyDifficultyError(float score)
    {
        // Zorluk seviyesine g�re hesaplama hatas� uygula
        switch (aiCompanyData.difficultyLevel)
        {
            case 2:
                score *= Random.Range(0.9f, 1.1f);
                break;
            case 3:
                score *= Random.Range(0.8f, 1.2f);
                break;
            case 4:
                score *= Random.Range(0.7f, 1.3f);
                break;
            default:
                // 1. seviye i�in herhangi bir hata yok
                break;
        }
        return score;
    }

    bool CanAffordIhale(IhaleData ihale)
    {
        return aiCompanyData.totalMoney >= ihale.ihaleFiyati && aiCompanyData.totalWorkers >= ihale.gerekliIsciler;
    }

    void PurchaseIhale(IhaleData ihale)
    {
        int requiredWorkers = ihale.gerekliIsciler;
        int requiredAdditionalWorkers = requiredWorkers - aiCompanyData.totalWorkers;

        if (requiredAdditionalWorkers > 0)
        {
            int workersToAdd = Mathf.Min(maxWorkerAdjustment, requiredAdditionalWorkers);
            for (int i = 0; i < workersToAdd; i++)
            {
                if (aiCompanyData.totalMoney >= workerCost)
                {
                    aiCompanyData.totalMoney -= workerCost;
                    aiCompanyData.totalWorkers++;
                }
            }
        }

        aiCompanyData.totalMoney -= ihale.ihaleFiyati;
        aiCompanyData.purchasedIhaleler.Add(ihale);
        availableIhaleler.Remove(ihale); // Al�nan ihaleyi availableIhaleler listesinden ��kar
        Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' ihaleyi sat�n ald� ve i��ileri atad�.");

        // �halenin ba�ar�s�n� de�erlendir
        EvaluateIhaleResult(ihale, ihale.gerekliIsciler);
    }

    void EvaluateIhaleResult(IhaleData ihale, int assignedWorkers)
    {
        float successChance = ihale.gerceklesmeOrani / 100f;
        successChance += assignedWorkers * 0.02f; // Her i��i i�in %5 art��

        Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' i�in ba�ar� y�zdesi: {successChance * 100}%");

        if (Random.value <= successChance)
        {
            // Ba�ar�l�
            float reward = ihale.ihaleFiyati + (ihale.ihaleFiyati * 0.5f);
            aiCompanyData.totalMoney += reward;
            Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' ihalede ba�ar�l� oldu ve {reward} para kazand�.");
        }
        else
        {
            // Ba�ar�s�z
            Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' ihalede ba�ar�s�z oldu.");
        }
    }

    bool AssignWorkersForIhale(IhaleData ihale)
    {
        if (aiCompanyData.totalWorkers >= ihale.gerekliIsciler)
        {
            aiCompanyData.totalWorkers -= ihale.gerekliIsciler;
            return true;
        }
        else
        {
            Debug.LogWarning($"AI '{aiCompanyData.companyName}' yeterli i��i bulamad�.");
            return false;
        }
    }
}
