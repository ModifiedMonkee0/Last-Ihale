using System;
using System.Collections;
using UnityEngine;

public class IhaleCorotine : MonoBehaviour
{
    public event Action<float> OnTimerTick; // Her saniyede bir çaðrýlacak olay

    public IEnumerator IhaleSonucuCoroutine()
    {
        float remainingTime = 60f; // Baþlangýçta kalan süre 60 saniye
        while (remainingTime > 0f)
        {
            // Her saniyede bir kalan süreyi azalt
            remainingTime -= Time.deltaTime;

            // Olayý tetikle ve kalan süreyi dinleyenlere aktar
            OnTimerTick?.Invoke(remainingTime);

            yield return null; // Bir sonraki frame'i bekle
        }

        // Zaman dolduðunda sonucu belirle
        bool isSuccessful = UnityEngine.Random.value > 0.5f;

        if (isSuccessful)
        {
            Debug.Log("Ihale iyi sonuçlandý.");
        }
        else
        {
            Debug.Log("Ihale kötü sonuçlandý.");
        }
    }
}