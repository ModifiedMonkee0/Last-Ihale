using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenPC : MonoBehaviour
{
    public GameObject Player;
    public GameObject usePC;

    public GameObject playerCamera;
    public GameObject pcCamera;

    public GameObject currentPara;
    public TMP_Text currentMoney;
    public PlayerData playerData;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            usePC.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                playerCamera.SetActive(false);
                pcCamera.SetActive(true);
                PcOpened();
                
                //pc u� para aktif
                currentPara.SetActive(true);
            }
        }

    }

    private void Update()
    {

        currentMoney.text = "Suanki Paran: " + playerData.currentMoney.ToString("C2");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            usePC.SetActive(false);
        }
    }

    public void PcOpened()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Player.SetActive(false);
    }

    


}
