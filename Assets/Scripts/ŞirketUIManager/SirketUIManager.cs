using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SirketUIManager : MonoBehaviour
{
    public IhaleCoroutine ihaleCoroutine;

    // Her bir ihale için UI bileþenlerini tanýmla
    public TMP_Text[] remainingTimeTexts = new TMP_Text[3];
    public TMP_Text[] ihaleAdiTexts = new TMP_Text[3];
    private bool[] slotsOccupied = new bool[3]; // Slotlarýn doluluk durumu

    void Start()
    {
        // IhaleCoroutine bileþenini dinamik olarak bul
        if (ihaleCoroutine == null)
        {
            ihaleCoroutine = FindObjectOfType<IhaleCoroutine>();
        }

        // IhaleCoroutine sýnýfýndaki OnTimerTick ve OnIhaleCompleted olaylarýna abone ol
        if (ihaleCoroutine != null)
        {
            ihaleCoroutine.OnTimerTick += UpdateRemainingTimeUI;
            ihaleCoroutine.OnIhaleCompleted += ClearIhaleUI;
        }
        else
        {
            Debug.LogError("IhaleCoroutine bileþeni bulunamadý.");
        }
    }

    // Kalan süreyi güncelleyen metot
    void UpdateRemainingTimeUI(float remainingTime, int index)
    {
        if (index >= 0 && index < remainingTimeTexts.Length)
        {
            remainingTimeTexts[index].text = Mathf.CeilToInt(remainingTime).ToString(); // Saniyeyi yuvarla ve UI'da göster
        }
    }

    // Ýlk boþ slotu bulan metot
    public int GetFirstAvailableSlot()
    {
        for (int i = 0; i < slotsOccupied.Length; i++)
        {
            if (!slotsOccupied[i])
            {
                return i;
            }
        }
        return -1; // Tüm slotlar doluysa -1 döner
    }

    public void SetIhaleAdi(string ihaleAdi, int index)
    {
        if (index >= 0 && index < ihaleAdiTexts.Length)
        {
            ihaleAdiTexts[index].text = ihaleAdi;
            slotsOccupied[index] = true; // Slotu dolu olarak iþaretle
        }
    }

    // Ýhale tamamlandýðýnda ilgili UI elemanlarýný temizleyen metot
    void ClearIhaleUI(int index)
    {
        if (index >= 0 && index < ihaleAdiTexts.Length)
        {
            ihaleAdiTexts[index].text = "Ýhale yok";
            remainingTimeTexts[index].text = "--";
            slotsOccupied[index] = false; // Slotu boþ olarak iþaretle
        }
    }
}
