using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Door doorScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // "E" tuþuna basýldýðýnda kapýyý aç/kapat
        {
            doorScript.MoveMyDoor();
        }
    }
}