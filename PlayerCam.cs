using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       //Blokuje kursor na œrodku ekranu
        Cursor.visible = false;                         //Ustawia kursor jako niewidoczny
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;        //uzyskanie danych wejœciowych z naszej myszki (w poziomie (X)) i u¿ycie wspó³czynnika czu³oœci myszy
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;        //uzyskanie danych wejœciowych z naszej myszki (w pionie (Y)) i u¿ycie wspó³czynnika czu³oœci myszy

        yRotation += mouseX;                                                        //dodanie wartoœci zapisanej w zmiennej mouseX do wartoœci zapisanie w zmiennej yRotation
        xRotation -= mouseY;                                                        //odejmuje wartoœæ zmiennej mouseY od wartoœcio w zmiennej xRotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);                              //ograniczenie zakresu zmiennej xRotation miêdzy -90 a 90 stopni

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);             //zamiana obrotu Transform.rotation za pomoc¹ wartoœci xRotation i yRotation == pozwala na obrót tylko w osi x i y a z ustawia na 0
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);                   //zamienia obrót obiektu orientacji za pomoc¹ yRotation i ustawia tylko obrót wokó³ osi y, ustawiaj¹c obrót x i z na 0
    }
}