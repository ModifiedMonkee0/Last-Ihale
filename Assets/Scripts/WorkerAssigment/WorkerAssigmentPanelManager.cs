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

    private List<WorkerData> assignedWorkers = new List<WorkerData>();

    void Start()
    {
        assignButton.onClick.AddListener(AssignWorkers);
        panel.SetActive(false); // Ba�lang��ta panel kapal�
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
        // ���ileri atama i�lemleri
        assignedWorkers.Clear(); // �nceki atamalar� temizle

        // �rne�in, kullan�c� aray�z�nden se�ilen i��ileri assignedWorkers listesine ekleyebilirsiniz
        // �rnek olarak atanan i��ileri ekleyelim
        int requiredGoodEngineers = 1; // �htiya� duyulan iyi m�hendis say�s�
        int requiredBadEngineers = 1; // �htiya� duyulan k�t� m�hendis say�s�
        int requiredGoodWorkers = 1; // �htiya� duyulan iyi i��i say�s�
        int requiredBadWorkers = 1; // �htiya� duyulan k�t� i��i say�s�

        if (playerData.goodEngineerData.count >= requiredGoodEngineers)
        {
            assignedWorkers.Add(playerData.goodEngineerData); // �rnek olarak t�m�n� ekliyoruz, ihtiyaca g�re se�ebilirsiniz
            playerData.goodEngineerData.count -= requiredGoodEngineers; // Say�y� azalt�yoruz
        }

        if (playerData.badEngineerData.count >= requiredBadEngineers)
        {
            assignedWorkers.Add(playerData.badEngineerData);
            playerData.badEngineerData.count -= requiredBadEngineers;
        }

        if (playerData.goodWorkerData.count >= requiredGoodWorkers)
        {
            assignedWorkers.Add(playerData.goodWorkerData);
            playerData.goodWorkerData.count -= requiredGoodWorkers;
        }

        if (playerData.badWorkerData.count >= requiredBadWorkers)
        {
            assignedWorkers.Add(playerData.badWorkerData);
            playerData.badWorkerData.count -= requiredBadWorkers;
        }

        // Atama i�lemi sonras� paneli kapat
        ClosePanel();

        // Ihale sat�n alma ve i��i atama i�lemleri
        playerData.currentMoney -= currentIhale.ihaleFiyati;
        playerData.workerCount -= currentIhale.gerekliIsciler;
        playerData.SaveData();

        // �haleyi ba�lat
        playerData.ihaleCoroutine.StartIhale(currentSlotIndex, currentIhale, assignedWorkers.ToArray());

        // �irketUIManager'a ihale ad�n� g�nderme
        playerData.sirketUIManager.SetIhaleAdi(currentIhale.ihaleAdi, currentSlotIndex);

        // ���i say�s�n� g�ncelle
        UpdateWorkerCounts();
    }

    private void UpdateWorkerCounts()
    {
        goodEngineerCountText.text = playerData.goodEngineerData.count.ToString();
        badEngineerCountText.text = playerData.badEngineerData.count.ToString();
        goodWorkerCountText.text = playerData.goodWorkerData.count.ToString();
        badWorkerCountText.text = playerData.badWorkerData.count.ToString();
    }
}
