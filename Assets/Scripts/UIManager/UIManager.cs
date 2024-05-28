using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //PC UI
    public Button _ihaleEkranButon;
    public Button _sinkedInEkranButon;
    public Button _sirketEkranBuyon;

    public GameObject _ihalePanel;
    public GameObject _sinkedInPanel;
    public GameObject _SirketPanel;

    public GameObject pcUI;
    //Pc UI

    //Karakter UI
    public TMP_Text currentMoney;
    public TMP_Text hourlyWage;
    public TMP_Text timerText;
    public PlayerData playerData;
    public GameObject karakterUI;
    //Karakter UI

    private WorkerPaymentScheduler workerPaymentScheduler;

    private void Start()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(true);
        _SirketPanel.SetActive(false);

        workerPaymentScheduler = FindObjectOfType<WorkerPaymentScheduler>();
        workerPaymentScheduler.OnTimerTick += UpdateTimer;

        UpdateHourlyWage(); // Baþlangýçta saatlik ücreti güncelle
    }

    private void OnDestroy()
    {
        workerPaymentScheduler.OnTimerTick -= UpdateTimer;
    }

    public void SinkedSayfaInAc()
    {
        _sinkedInPanel.SetActive(true);
        _ihalePanel.SetActive(false);
        _SirketPanel.SetActive(false);
    }

    public void IhalelerSayfaAc()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(true);
        _SirketPanel.SetActive(false);
    }

    public void SirketSayfaAc()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(false);
        _SirketPanel.SetActive(true);
    }

    //PC UI

    //Karakter UI
    private void Update()
    {
        currentMoney.text = "Suanki Paran: " + playerData.currentMoney.ToString("C2");
        UpdateHourlyWage();
    }

    private void UpdateTimer(float timer)
    {
        timerText.text = "Sonraki ödeme: " + Mathf.Ceil(timer).ToString() + " saniye";
    }

    private void UpdateHourlyWage()
    {
        hourlyWage.text = "Saatlik Ücret: " + playerData.totalHourlyWage.ToString("C2");
    }
}
