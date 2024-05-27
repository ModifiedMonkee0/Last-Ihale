using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SirketUIManager : MonoBehaviour
{
    public IhaleCorotine ihaleCorotine;
    public TMP_Text remainingTimeText;
    public TMP_Text ihaleAdiText;


    void Start()
    {
        // IhaleCorotine sýnýfýndaki OnTimerTick olayýna abone ol
        ihaleCorotine.OnTimerTick += UpdateRemainingTimeUI;
    }

    // Kalan süreyi güncelleyen metot
    void UpdateRemainingTimeUI(float remainingTime)
    {
        remainingTimeText.text = Mathf.CeilToInt(remainingTime).ToString(); // Saniyeyi yuvarla ve UI'da göster
    }

    public void SetIhaleAdi(string ihaleAdi)
    {
        ihaleAdiText.text = ihaleAdi;
    }
}