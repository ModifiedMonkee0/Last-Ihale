using System.Collections;
using UnityEngine;
using System;

public class WorkerPaymentScheduler : MonoBehaviour
{
    private PlayerData playerData;
    public event Action<float> OnTimerTick;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>(); // PlayerData scriptine referans al�n
        StartCoroutine(PayWorkersRoutine()); // Coroutine'i ba�lat
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
            playerData.PayWorkers(); // ���ilere �deme yap
        }
    }
}
