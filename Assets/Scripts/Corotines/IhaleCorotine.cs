using System;
using System.Collections;
using UnityEngine;

public class IhaleCorotine : MonoBehaviour
{
    public event Action<float> OnTimerTick; // Her saniyede bir �a�r�lacak olay

    public IEnumerator IhaleSonucuCoroutine()
    {
        float remainingTime = 60f; // Ba�lang��ta kalan s�re 60 saniye
        while (remainingTime > 0f)
        {
            // Her saniyede bir kalan s�reyi azalt
            remainingTime -= Time.deltaTime;

            // Olay� tetikle ve kalan s�reyi dinleyenlere aktar
            OnTimerTick?.Invoke(remainingTime);

            yield return null; // Bir sonraki frame'i bekle
        }

        // Zaman doldu�unda sonucu belirle
        bool isSuccessful = UnityEngine.Random.value > 0.5f;

        if (isSuccessful)
        {
            Debug.Log("Ihale iyi sonu�land�.");
        }
        else
        {
            Debug.Log("Ihale k�t� sonu�land�.");
        }
    }
}