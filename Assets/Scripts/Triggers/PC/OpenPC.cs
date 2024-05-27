using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenPC : MonoBehaviour
{
    public UIManager uıManager;

    public GameObject Player;
    public GameObject usePC;

    public GameObject playerCamera;
    public GameObject pcCamera;

    public GameObject currentPara;
    public TMP_Text currentMoney;
    public PlayerData playerData;

    private bool playerInRange = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            usePC.SetActive(true);
            playerInRange = true;

        }

    }

    private void Update()
    {
       currentMoney.text = "Suanki Paran: " + playerData.currentMoney.ToString("C2");

        //ınput manager?
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            playerCamera.SetActive(false);
            pcCamera.SetActive(true);
            PcOpened();

            
            currentPara.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            usePC.SetActive(false);
            playerInRange = false;
        }
    }

    public void PcOpened()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Player.SetActive(false);
        uıManager.karakterUI.SetActive(false);
    }

    


}
