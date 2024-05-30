using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySoundManager : MonoBehaviour
{
    
    public GameObject citySFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            citySFX.SetActive(true);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
      

        if (other.CompareTag("Player"))
        {
            citySFX.SetActive(false);
        }
    }
}
