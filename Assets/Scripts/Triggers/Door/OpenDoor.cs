using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Door doorScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // "E" tu�una bas�ld���nda kap�y� a�/kapat
        {
            doorScript.MoveMyDoor();
        }
    }
}