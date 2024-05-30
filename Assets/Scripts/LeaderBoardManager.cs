using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderBoardPanel;
    public PlayerData playerData;
    public List<AICompanyData> aiCompanies;
    public List<LeaderboardEntry> leaderboardEntries; // Leaderboard UI elemanlarý
    private bool isleaderBoardPanelOpen;

    private void Update()
    {
        UpdateLeaderboard();

        if(Input.GetKeyDown(KeyCode.L) && !isleaderBoardPanelOpen)
        {
            isleaderBoardPanelOpen = true;
            OpenLeaderBoard();
        }
        else if (Input.GetKeyDown(KeyCode.L) && isleaderBoardPanelOpen)
        {
            isleaderBoardPanelOpen = false;
            CloseLeaderBoard();
        }
    }
    private void Start()
    {
        isleaderBoardPanelOpen = false;
    }
    
    public void OpenLeaderBoard()
    {
        leaderBoardPanel.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        leaderBoardPanel.SetActive(false);
    }

    public void UpdateLeaderboard()
    {
        // Tüm þirketleri ve oyuncuyu bir listede topla
        List<CompanyInfo> allCompanies = new List<CompanyInfo>();

        // Oyuncu bilgilerini ekle
        allCompanies.Add(new CompanyInfo(playerData.currentMoney, "Player"));

        // AI þirket bilgilerini ekle
        foreach (AICompanyData aiCompany in aiCompanies)
        {
            allCompanies.Add(new CompanyInfo(aiCompany.totalMoney, aiCompany.companyName));
        }

        // Þirketleri paraya göre sýrala (büyükten küçüðe)
        allCompanies.Sort((x, y) => y.currentMoney.CompareTo(x.currentMoney));

        // Sýralamayý UI'a güncelle
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            if (i < allCompanies.Count)
            {
                leaderboardEntries[i].SetEntry(allCompanies[i].companyName, allCompanies[i].currentMoney);
            }
            else
            {
                leaderboardEntries[i].ClearEntry();
            }
        }
    }
}

[System.Serializable]
public class CompanyInfo
{
    public float currentMoney;
    public string companyName;

    public CompanyInfo(float currentMoney, string companyName)
    {
        this.currentMoney = currentMoney;
        this.companyName = companyName;
    }
}

// UI için basit bir sýnýf


[System.Serializable]
public class LeaderboardEntry
{
    public TMP_Text companyNameText;
    public TMP_Text currentMoneyText;

    public void SetEntry(string companyName, float currentMoney)
    {
        companyNameText.text = companyName;
        currentMoneyText.text = currentMoney.ToString("F2"); // Ýki ondalýk basamak göster
    }

    public void ClearEntry()
    {
        companyNameText.text = "";
        currentMoneyText.text = "";
    }
}

