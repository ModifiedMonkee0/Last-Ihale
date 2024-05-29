using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompany : MonoBehaviour
{
    public AICompanyData aiCompanyData;
    public List<IhaleData> availableIhaleler;
    public float checkInterval = 10f; // AI'nin ihaleleri kontrol etme sýklýðý
    public float workerAdjustmentInterval = 30f; // AI'nin iþçi sayýsýný ayarlama sýklýðý
    public int workerCost = 5000; // Her iþçi için maliyet
    public int workerMaintenanceCost = 1000; // Her 30 saniyede her iþçi için maliyet
    public int maxWorkerAdjustment = 3; // Her seferinde maksimum ayarlanabilir iþçi sayýsý
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
        Debug.Log($"AI '{aiCompanyData.companyName}' {aiCompanyData.totalWorkers} iþçi için {totalCost} bakým ücreti ödedi. Kalan para: {aiCompanyData.totalMoney}");
    }

    IhaleData SelectBestIhale()
    {
        IhaleData bestIhale = null;
        float bestScore = float.MinValue;

        foreach (IhaleData ihale in availableIhaleler)
        {
            float score = CalculateIhaleScore(ihale);
            score = ApplyDifficultyError(score);
            Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' için hesaplanan skor: {score}");

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

        // Ýþçi baþýna %5 baþarý artýþý
        successChance += requiredWorkers * 0.05f;

        // Karlýlýk skoru hesaplama (potansiyel kar * baþarý þansý)
        return potentialProfit * successChance - ihale.ihaleFiyati;
    }

    float ApplyDifficultyError(float score)
    {
        // Zorluk seviyesine göre hesaplama hatasý uygula
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
                // 1. seviye için herhangi bir hata yok
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
        availableIhaleler.Remove(ihale); // Alýnan ihaleyi availableIhaleler listesinden çýkar
        Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' ihaleyi satýn aldý ve iþçileri atadý.");

        // Ýhalenin baþarýsýný deðerlendir
        EvaluateIhaleResult(ihale, ihale.gerekliIsciler);
    }

    void EvaluateIhaleResult(IhaleData ihale, int assignedWorkers)
    {
        float successChance = ihale.gerceklesmeOrani / 100f;
        successChance += assignedWorkers * 0.02f; // Her iþçi için %5 artýþ

        Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' için baþarý yüzdesi: {successChance * 100}%");

        if (Random.value <= successChance)
        {
            // Baþarýlý
            float reward = ihale.ihaleFiyati + (ihale.ihaleFiyati * 0.5f);
            aiCompanyData.totalMoney += reward;
            Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' ihalede baþarýlý oldu ve {reward} para kazandý.");
        }
        else
        {
            // Baþarýsýz
            Debug.Log($"AI '{aiCompanyData.companyName}' '{ihale.ihaleAdi}' ihalede baþarýsýz oldu.");
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
            Debug.LogWarning($"AI '{aiCompanyData.companyName}' yeterli iþçi bulamadý.");
            return false;
        }
    }
}
