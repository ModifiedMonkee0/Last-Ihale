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
        // IhaleCorotine s�n�f�ndaki OnTimerTick olay�na abone ol
        ihaleCorotine.OnTimerTick += UpdateRemainingTimeUI;
    }

    // Kalan s�reyi g�ncelleyen metot
    void UpdateRemainingTimeUI(float remainingTime)
    {
        remainingTimeText.text = Mathf.CeilToInt(remainingTime).ToString(); // Saniyeyi yuvarla ve UI'da g�ster
    }

    public void SetIhaleAdi(string ihaleAdi)
    {
        ihaleAdiText.text = ihaleAdi;
    }
}