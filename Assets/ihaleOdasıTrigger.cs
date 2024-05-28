using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ihaleOdasıTrigger : MonoBehaviour
{
    public GameObject Player;
    //ışınlan transform
    public Transform ihaleOdası;
    public Transform sehirTransform;
    //araba controller acıska kapat

    public GameObject arabaController;
    //karakter kontrolelr kapalıysa aç
    public GameObject karakterController;

    private bool playerInRange = false;

    //text
    public GameObject ihaleOdasinaGir;

    public GameObject screenSpaceUI;

    public bool playerIhalede = false;

    private void Start()
    {
        playerIhalede = false;
    }
    private void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            //ihaleOdasinaGir.SetActive(true);
            //karakterController.SetActive(true);
            //arabaController.SetActive(false);
            if(playerIhalede==true)
            {
                PlayerıSehireIsınla();
            }
            else if (playerIhalede == false)
            {
                PlayerıIhaleyeIsınla();
            }
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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

    public void PlayerıIhaleyeIsınla()
    {
        Player.transform.position = ihaleOdası.position;
        Player.transform.rotation = ihaleOdası.rotation;
        playerIhalede = true;
    }

    public void PlayerıSehireIsınla()
    {
        Player.transform.position = sehirTransform.position;
        Player.transform.rotation = sehirTransform.rotation;
        playerIhalede = false;
    }


}
