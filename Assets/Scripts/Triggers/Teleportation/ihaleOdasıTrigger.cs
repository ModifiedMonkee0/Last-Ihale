using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ihaleOdasiTrigger : MonoBehaviour
{
    public GameObject player;
    
    public Transform sehirTransform;

    
    private bool playerInRange = false;

    public GameObject uýgirCik;
    public GameObject screenSpaceUI;

    

   

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {


            PlayeriSehreIsinla();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            screenSpaceUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            screenSpaceUI.SetActive(false);
        }
    }

    private void PlayeriSehreIsinla()
    {
        player.transform.position = sehirTransform.position;
        player.transform.rotation = sehirTransform.rotation;
        

        
        
    }

    
}
