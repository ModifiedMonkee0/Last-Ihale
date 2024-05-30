using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WorkerUIManager : MonoBehaviour
{
    public WorkerData[] workerDataArray; // ���i verileri
    public PlayerData playerData;        // Oyuncu verileri
    //public WorkerAssignmentPanelManager workerAssignmentPanelManager;

    // UI referanslar�
    public TMP_Text[] workerNameArray = new TMP_Text[50];
    public TMP_Text[] hiringCostArray = new TMP_Text[50];
    public TMP_Text[] hourlyWageArray = new TMP_Text[50];
    public Button[] hireWorkerButtonArray = new Button[50];
    public Button[] sellWorkerButtonArray = new Button[50]; // Yeni eklenen butonlar

    void Start()
    {
        for (int i = 0; i < workerDataArray.Length && i < 50; i++)
        {
            if (workerDataArray[i] != null)
            {
                // ���i bilgilerini UI elemanlar�na atama
                workerNameArray[i].text = workerDataArray[i].workerName;
                hiringCostArray[i].text = workerDataArray[i].hiringCost.ToString("C2");
                hourlyWageArray[i].text = workerDataArray[i].hourlyWage.ToString("C2");

                int index = i;  // ���inin indeksini yakalamak i�in
                hireWorkerButtonArray[i].onClick.AddListener(() => HireWorker(index));
                sellWorkerButtonArray[i].onClick.AddListener(() => SellWorker(index)); // Sat�� butonu i�in listener ekle
            }
        }
    }

    void HireWorker(int index)
    {
        //workerAssignmentPanelManager.UpdateWorkerCounts();
        // ���i say�s�n� UI'da g�ncelle

        WorkerData selectedWorker = workerDataArray[index];

        if (playerData.HireWorker(selectedWorker))
        {
            Debug.Log("���i ba�ar�yla sat�n al�nd�: " + selectedWorker.workerName);
        }
        else
        {
            Debug.Log("Yeterli para yok: " + selectedWorker.workerName);
        }
    }

    void SellWorker(int index)
    {
        WorkerData selectedWorker = workerDataArray[index];

        if (playerData.SellWorker(selectedWorker))
        {
            Debug.Log("���i ba�ar�yla sat�ld�: " + selectedWorker.workerName);
        }
        else
        {
            Debug.Log("Sat�lacak i��i yok: " + selectedWorker.workerName);
        }
    }
}
