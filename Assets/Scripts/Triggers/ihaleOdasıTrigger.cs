using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhaleOdasiTrigger : MonoBehaviour
{
    public GameObject player;
    public Transform ihaleOdasi;
    public Transform sehirTransform;

    
    

    private bool playerInRange = false;

    public GameObject ihaleOdasinaGir;
    public GameObject screenSpaceUI;

    private bool playerIhalede = false;

    private void Start()
    {
        playerIhalede = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (playerIhalede)
            {
                PlayeriSehireIsinla();
            }
            else
            {
                PlayeriIhaleyeIsinla();
            }
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

    private void PlayeriIhaleyeIsinla()
    {
        player.transform.position = ihaleOdasi.position;
        player.transform.rotation = ihaleOdasi.rotation;
        playerIhalede = true;

        
        
    }

    private void PlayeriSehireIsinla()
    {
        player.transform.position = sehirTransform.position;
        player.transform.rotation = sehirTransform.rotation;
        playerIhalede = false;

       
        
    }
}
