using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SirketUIManager : MonoBehaviour
{
    public IhaleCoroutine ihaleCoroutine;

    // Her bir ihale i�in UI bile�enlerini tan�mla
    public TMP_Text[] remainingTimeTexts = new TMP_Text[3];
    public TMP_Text[] ihaleAdiTexts = new TMP_Text[3];
    public TMP_Text[] completionRateTexts = new TMP_Text[3]; // Final completion rate i�in ekledik

    private bool[] slotsOccupied = new bool[3]; // Slotlar�n doluluk durumu

    private void Update()
    {
        
    }
    void Start()
    {
        // IhaleCoroutine bile�enini dinamik olarak bul
        if (ihaleCoroutine == null)
        {
            ihaleCoroutine = FindObjectOfType<IhaleCoroutine>();
        }

        // IhaleCoroutine s�n�f�ndaki OnTimerTick ve OnIhaleCompleted olaylar�na abone ol
        if (ihaleCoroutine != null)
        {

            ihaleCoroutine.OnTimerTick += UpdateRemainingTimeUI;
            ihaleCoroutine.OnIhaleCompleted += ClearIhaleUI;
        }
        else
        {
            Debug.LogError("IhaleCoroutine bile�eni bulunamad�.");
        }

        // Ba�lang��ta UI bile�enlerini temizle
        ClearAllIhaleUI();
    }

    // Kalan s�reyi g�ncelleyen metot
    void UpdateRemainingTimeUI(float remainingTime, int index)
    {
        if (index >= 0 && index < remainingTimeTexts.Length)
        {
            remainingTimeTexts[index].text = Mathf.CeilToInt(remainingTime).ToString(); // Saniyeyi yuvarla ve UI'da g�ster
        }
    }

    // �lk bo� slotu bulan metot
    public int GetFirstAvailableSlot()
    {
        for (int i = 0; i < slotsOccupied.Length; i++)
        {
            if (!slotsOccupied[i])
            {
                return i;
            }
        }
        return -1; // T�m slotlar doluysa -1 d�ner
    }

    public void SetIhaleAdi(string ihaleAdi, int index)
    {
        if (index >= 0 && index < ihaleAdiTexts.Length)
        {
            ihaleAdiTexts[index].text = ihaleAdi;
            slotsOccupied[index] = true; // Slotu dolu olarak i�aretle
        }
    }

    // �hale tamamland���nda ilgili UI elemanlar�n� temizleyen metot
    void ClearIhaleUI(int index)
    {
        if (index >= 0 && index < ihaleAdiTexts.Length)
        {
            ihaleAdiTexts[index].text = "�hale yok";
            remainingTimeTexts[index].text = "--";
            completionRateTexts[index].text = "--"; // Final completion rate'i temizle
            slotsOccupied[index] = false; // Slotu bo� olarak i�aretle
        }
    }

    // T�m UI elemanlar�n� temizleyen metot
    void ClearAllIhaleUI()
    {
        for (int i = 0; i < ihaleAdiTexts.Length; i++)
        {
            ClearIhaleUI(i);
        }
    }

    // Final completion rate'i g�ncelleyen metot
    public void UpdateCompletionRateUI(float completionRate, int index)
    {
        if (index >= 0 && index < completionRateTexts.Length)
        {
           
            completionRateTexts[index].text = completionRate.ToString("F2") + "%";
        }
    }
}
