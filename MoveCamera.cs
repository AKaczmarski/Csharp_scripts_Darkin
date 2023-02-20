using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
        //cameraPosition to zmienna która przechowuje pozycjê okreœlonej kamery
        //aktualizacja pozycji obiektu w ka¿dej klatce (Update) tak aby dopasowaæ pozycjê kamery reprezentowan¹ przez cameraPosition
        //kamera zawsze porusza siê z graczem
    }
}
