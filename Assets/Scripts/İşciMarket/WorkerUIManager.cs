using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WorkerUIManager : MonoBehaviour
{
    public WorkerData[] workerDataArray; // Ýþçi verileri
    public PlayerData playerData;        // Oyuncu verileri

    // UI referanslarý
    public TMP_Text[] workerNameArray = new TMP_Text[50];
    public TMP_Text[] hiringCostArray = new TMP_Text[50];
    public TMP_Text[] hourlyWageArray = new TMP_Text[50];
    public Button[] hireWorkerButtonArray = new Button[50];

    void Start()
    {
        for (int i = 0; i < workerDataArray.Length && i < 50; i++)
        {
            if (workerDataArray[i] != null)
            {
                // Ýþçi bilgilerini UI elemanlarýna atama
                workerNameArray[i].text = workerDataArray[i].workerName;
                hiringCostArray[i].text = workerDataArray[i].hiringCost.ToString("C2");
                hourlyWageArray[i].text = workerDataArray[i].hourlyWage.ToString("C2");

                int index = i;  // Ýþçinin indeksini yakalamak için
                hireWorkerButtonArray[i].onClick.AddListener(() => HireWorker(index));
            }
        }
    }

    void HireWorker(int index)
    {
        WorkerData selectedWorker = workerDataArray[index];

        if (playerData.HireWorker(selectedWorker))
        {
            Debug.Log("Ýþçi baþarýyla satýn alýndý: " + selectedWorker.workerName);
        }
        else
        {
            Debug.Log("Yeterli para yok: " + selectedWorker.workerName);
        }
    }
}
