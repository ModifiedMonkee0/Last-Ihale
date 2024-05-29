using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ihaleBinasıTrigger : MonoBehaviour
{
    public GameObject player;

    public Transform ihaleOdasiTransform;

    private bool playerInRange = false;

    public GameObject uıgirCik;
    public GameObject screenSpaceUI;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {


            PlayeriIhaleyeIsinla();

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
        player.transform.position = ihaleOdasiTransform.position;
        player.transform.rotation = ihaleOdasiTransform.rotation;
    }
   

}
