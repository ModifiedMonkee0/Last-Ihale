using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IhaleUIManager : MonoBehaviour
{
    public IhaleData[] ihaleDataArray;  // ScriptableObject referanslarý
    public PlayerData playerData;       // Oyuncu verileri referansý
    public WorkerAssignmentPanelManager workerAssignmentPanel; // Worker Assignment Panel referansý

    // 50 adet ihale isim, fiyat ve satýn alma butonu referanslarý
    public TMP_Text[] ihaleIsimArray = new TMP_Text[50];
    public TMP_Text[] ihaleFiyatArray = new TMP_Text[50];
    public Button[] ihaleSatinAlButtonArray = new Button[50];

    // IhaleCoroutine
    public IhaleCoroutine ihaleCoroutine;

    // ÞirketUIManager Referansý
    public SirketUIManager þirketUIManager;

    void Start()
    {
        for (int i = 0; i < ihaleDataArray.Length && i < 50; i++)
        {
            if (ihaleDataArray[i] != null)
            {
                // Ýhale bilgilerini UI elemanlarýna atama
                ihaleIsimArray[i].text = ihaleDataArray[i].ihaleAdi;
                ihaleFiyatArray[i].text = ihaleDataArray[i].ihaleFiyati.ToString("C2");

                int index = i;  // Ýhalenin indeksini yakalamak için
                ihaleSatinAlButtonArray[i].onClick.AddListener(() => SatinAl(index));
            }
        }
    }

    void SatinAl(int index)
    {
        IhaleData selectedIhale = ihaleDataArray[index];

        if (playerData.currentMoney >= selectedIhale.ihaleFiyati && playerData.workerCount >= selectedIhale.gerekliIsciler)
        {
            // Ýlk boþ slotu al
            int availableSlot = þirketUIManager.GetFirstAvailableSlot();
            if (availableSlot != -1)
            {
                // Ýþçi atama panelini aç
                workerAssignmentPanel.OpenPanel(selectedIhale, availableSlot, playerData);
            }
            else
            {
                Debug.Log("Tüm slotlar dolu. Yeni ihale alýnamýyor.");
            }
        }
        else
        {
            Debug.Log("Yeterli para veya iþçi yok: " + selectedIhale.ihaleAdi);
        }
    }
}
