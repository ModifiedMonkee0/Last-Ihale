using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCar : MonoBehaviour
{

    public GameObject Player;

    public GameObject prometheus;
    public GameObject carMesh;


    private bool playerInRange = false;
    private bool playerInCar = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerInRange = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            playerInRange = false;
        }
    }

    private void Update()
    {
        

        //ýnput manager?
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            Player.SetActive(false);
            prometheus.SetActive(true);
            carMesh.SetActive(false);

            playerInCar = true;

            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && playerInCar)
        {
            // carMesh'in pozisyonunu ve rotasyonunu prometheus'un pozisyonu ve rotasyonuna eþitle
            carMesh.transform.position = prometheus.transform.position;
            carMesh.transform.rotation = prometheus.transform.rotation;

            // Player'ýn pozisyonunu ve rotasyonunu prometheus'un pozisyonu ve rotasyonuna eþitle
            Player.transform.position = prometheus.transform.position + new Vector3(0, 0, 1);
            Player.transform.rotation = prometheus.transform.rotation;

            Player.SetActive(true);
            prometheus.SetActive(false);
            carMesh.SetActive(true);
            playerInCar = false;

        }
    }
}
