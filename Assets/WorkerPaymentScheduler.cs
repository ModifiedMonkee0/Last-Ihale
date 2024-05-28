using System.Collections;
using UnityEngine;
using System;

public class WorkerPaymentScheduler : MonoBehaviour
{
    private PlayerData playerData;
    public event Action<float> OnTimerTick;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>(); // PlayerData scriptine referans alýn
        StartCoroutine(PayWorkersRoutine()); // Coroutine'i baþlat
    }

    IEnumerator PayWorkersRoutine()
    {
        while (true)
        {
            float timer = 30f;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                OnTimerTick?.Invoke(timer);
                yield return null;
            }
            playerData.PayWorkers(); // Ýþçilere ödeme yap
        }
    }
}
