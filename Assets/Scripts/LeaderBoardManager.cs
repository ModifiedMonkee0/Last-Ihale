using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderBoardPanel;
    public PlayerData playerData;
    public List<AICompanyData> aiCompanies;
    public List<LeaderboardEntry> leaderboardEntries; // Leaderboard UI elemanlar�
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
        // T�m �irketleri ve oyuncuyu bir listede topla
        List<CompanyInfo> allCompanies = new List<CompanyInfo>();

        // Oyuncu bilgilerini ekle
        allCompanies.Add(new CompanyInfo(playerData.currentMoney, "Player"));

        // AI �irket bilgilerini ekle
        foreach (AICompanyData aiCompany in aiCompanies)
        {
            allCompanies.Add(new CompanyInfo(aiCompany.totalMoney, aiCompany.companyName));
        }

        // �irketleri paraya g�re s�rala (b�y�kten k����e)
        allCompanies.Sort((x, y) => y.currentMoney.CompareTo(x.currentMoney));

        // S�ralamay� UI'a g�ncelle
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

// UI i�in basit bir s�n�f


[System.Serializable]
public class LeaderboardEntry
{
    public TMP_Text companyNameText;
    public TMP_Text currentMoneyText;

    public void SetEntry(string companyName, float currentMoney)
    {
        companyNameText.text = companyName;
        currentMoneyText.text = currentMoney.ToString("F2"); // �ki ondal�k basamak g�ster
    }

    public void ClearEntry()
    {
        companyNameText.text = "";
        currentMoneyText.text = "";
    }
}

