using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankSoundManager : MonoBehaviour
{
    public GameObject bankSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bankSFX.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            bankSFX.SetActive(false);
        }
    }
}
