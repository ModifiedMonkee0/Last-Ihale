using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IhaleUIManager : MonoBehaviour
{
    public IhaleData[] ihaleDataArray;  // ScriptableObject referanslar�
    public PlayerData playerData;       // Oyuncu verileri referans�
    public WorkerAssignmentPanelManager workerAssignmentPanel; // Worker Assignment Panel referans�

    // 50 adet ihale isim, fiyat ve sat�n alma butonu referanslar�
    public TMP_Text[] ihaleIsimArray = new TMP_Text[50];
    public TMP_Text[] ihaleFiyatArray = new TMP_Text[50];
    public TMP_Text[] ihaleM�hendisArray = new TMP_Text[50];
    public TMP_Text[] ihaleIsciArray = new TMP_Text[50];
    public TMP_Text[] ihaleGercekle�meOran� = new TMP_Text[50];

    public Button[] ihaleSatinAlButtonArray = new Button[50];

    public GameObject[] ihaleTutucular�Array;

    

    // IhaleCoroutine
    public IhaleCoroutine ihaleCoroutine;

    // �irketUIManager Referans�
    public SirketUIManager sirketUIManager; 

    void Start()
    {
        for (int i = 0; i < ihaleDataArray.Length && i < 50; i++)
        {
            if (ihaleDataArray[i] != null)
            {
                // �hale bilgilerini UI elemanlar�na atama
                ihaleIsimArray[i].text = ihaleDataArray[i].ihaleAdi;
                ihaleFiyatArray[i].text = ihaleDataArray[i].ihaleFiyati.ToString("C2");
                ihaleM�hendisArray[i].text = "Required Engineers: " + ihaleDataArray[i].gerekliMuhendisler.ToString();
                ihaleIsciArray[i].text = "Required Workers: " + ihaleDataArray[i].gerekliIsciler.ToString();
                ihaleGercekle�meOran�[i].text = "Success Change: " + ihaleDataArray[i].gerceklesmeOrani.ToString();



                // Arraydeki her bir butona o ihalenin index'ini at
                int index = i;  // �halenin indeksini yakalamak i�in buray� kulland�k.
                ihaleSatinAlButtonArray[i].onClick.AddListener(() => SatinAl(index));
            }
        }
    }

    public void SatinAl(int index)
    {
        IhaleData selectedIhale = ihaleDataArray[index];

        if (playerData.currentMoney >= selectedIhale.ihaleFiyati && playerData.workerCount >= selectedIhale.gerekliIsciler + selectedIhale.gerekliMuhendisler)
        {
            // �lk bo� slotu al
            int availableSlot = sirketUIManager.GetFirstAvailableSlot();
            if (availableSlot != -1)
            {
                // ���i atama panelini a�
                workerAssignmentPanel.OpenPanel(selectedIhale, availableSlot, playerData, index); // Index parametresi eklendi
            }
            else
            {
                Debug.Log("T�m slotlar dolu. Yeni ihale al�nam�yor.");
            }
        }
        else
        {
            Debug.Log("Yeterli para veya i��i yok: " + selectedIhale.ihaleAdi);
        }
    }
}
