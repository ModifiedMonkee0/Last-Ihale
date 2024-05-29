using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankaBinasıTrigger : MonoBehaviour
{
    public GameObject player;

    public Transform bankaOdasiTransform;

    private bool playerInRange = false;

    public GameObject uıgirCik;
    public GameObject screenSpaceUI;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {


            PlayeriBankayaIsinla();

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

    private void PlayeriBankayaIsinla()
    {
        player.transform.position = bankaOdasiTransform.position;
        player.transform.rotation = bankaOdasiTransform.rotation;
    }
}
