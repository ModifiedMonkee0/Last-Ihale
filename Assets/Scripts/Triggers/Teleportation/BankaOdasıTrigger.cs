using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankaOdasıTrigger : MonoBehaviour
{
    public GameObject player;

    public Transform bankaBinaTransform;


    private bool playerInRange = false;

    public GameObject uıgirCik;
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
        player.transform.position = bankaBinaTransform.position;
        player.transform.rotation = bankaBinaTransform.rotation;




    }
}
