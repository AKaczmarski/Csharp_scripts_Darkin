using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
        //cameraPosition to zmienna kt�ra przechowuje pozycj� okre�lonej kamery
        //aktualizacja pozycji obiektu w ka�dej klatce (Update) tak aby dopasowa� pozycj� kamery reprezentowan� przez cameraPosition
        //kamera zawsze porusza si� z graczem
    }
}
