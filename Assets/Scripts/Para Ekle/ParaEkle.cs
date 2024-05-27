using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaEkle : MonoBehaviour
{
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if( Input.GetKeyDown(KeyCode.P))
            {
            playerData.currentMoney += 50000f;
            }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerData.SaveData();
            Debug.Log("Game Saved");
        }
    }
}
