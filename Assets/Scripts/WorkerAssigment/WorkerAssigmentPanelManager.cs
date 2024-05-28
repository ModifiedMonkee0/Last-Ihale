using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkerAssignmentPanelManager : MonoBehaviour
{
    public GameObject panel; // Panel referansý
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
        panel.SetActive(false); // Baþlangýçta panel kapalý
    }

    public void OpenPanel(IhaleData ihaleData, int slotIndex, PlayerData player)
    {
        currentIhale = ihaleData;
        currentSlotIndex = slotIndex;
        playerData = player;
        ihaleAdiText.text = ihaleData.ihaleAdi;
        panel.SetActive(true);

        // Ýþçi sayýsýný UI'da güncelle
        UpdateWorkerCounts();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void AssignWorkers()
    {
        // Ýþçileri atama iþlemleri
        assignedWorkers.Clear(); // Önceki atamalarý temizle

        // Örneðin, kullanýcý arayüzünden seçilen iþçileri assignedWorkers listesine ekleyebilirsiniz
        // Örnek olarak atanan iþçileri ekleyelim
        int requiredGoodEngineers = 2; // Ýhtiyaç duyulan iyi mühendis sayýsý
        int requiredBadEngineers = 2; // Ýhtiyaç duyulan kötü mühendis sayýsý
        int requiredGoodWorkers = 2; // Ýhtiyaç duyulan iyi iþçi sayýsý
        int requiredBadWorkers = 2; // Ýhtiyaç duyulan kötü iþçi sayýsý

        if (playerData.goodEngineerData.count >= requiredGoodEngineers)
        {
            assignedWorkers.Add(playerData.goodEngineerData); // Örnek olarak tümünü ekliyoruz, ihtiyaca göre seçebilirsiniz
            playerData.goodEngineerData.count -= requiredGoodEngineers; // Sayýyý azaltýyoruz
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

        // Atama iþlemi sonrasý paneli kapat
        ClosePanel();

        // Ihale satýn alma ve iþçi atama iþlemleri
        playerData.currentMoney -= currentIhale.ihaleFiyati;
        playerData.workerCount -= currentIhale.gerekliIsciler;
        playerData.SaveData();

        // Ýhaleyi baþlat
        playerData.ihaleCoroutine.StartIhale(currentSlotIndex, currentIhale, assignedWorkers.ToArray());

        // ÞirketUIManager'a ihale adýný gönderme
        playerData.sirketUIManager.SetIhaleAdi(currentIhale.ihaleAdi, currentSlotIndex);

        // Ýþçi sayýsýný güncelle
        UpdateWorkerCounts();
    }

    private void UpdateWorkerCounts()
    {
        goodEngineerCountText.text = ("Ýyi Mühendis: " + playerData.goodEngineerData.count.ToString());
        badEngineerCountText.text = ("Kötü Mühendis:" + playerData.badEngineerData.count.ToString());
        goodWorkerCountText.text = ("Ýyi iþci: " + playerData.goodWorkerData.count.ToString());
        badWorkerCountText.text = ("Kötü Ýþci: " + playerData.badWorkerData.count.ToString());
    }
}