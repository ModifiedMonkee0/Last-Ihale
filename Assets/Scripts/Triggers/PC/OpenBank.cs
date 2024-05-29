using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBank : MonoBehaviour
{

    public UIManager uýManager;

    public GameObject Player;
    public GameObject playerCamera;
    public GameObject UseBanka;
    public GameObject bankaCamera;

    private bool playerInRange = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            playerCamera.SetActive(false);
            bankaCamera.SetActive(true);
            BankaOpened();


            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UseBanka.SetActive(true);
            playerInRange = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UseBanka.SetActive(false);
            playerInRange = false;
        }
    }

    void BankaOpened()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Player.SetActive(false);
        uýManager.karakterUI.SetActive(false);
    }

    public void BankaClosed()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Player.SetActive(true);
        uýManager.karakterUI.SetActive(true);
        bankaCamera.SetActive(false);
        playerCamera.SetActive(true);
    }
}
