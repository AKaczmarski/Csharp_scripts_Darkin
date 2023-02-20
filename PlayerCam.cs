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
        Cursor.lockState = CursorLockMode.Locked;       //Blokuje kursor na �rodku ekranu
        Cursor.visible = false;                         //Ustawia kursor jako niewidoczny
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;        //uzyskanie danych wej�ciowych z naszej myszki (w poziomie (X)) i u�ycie wsp�czynnika czu�o�ci myszy
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;        //uzyskanie danych wej�ciowych z naszej myszki (w pionie (Y)) i u�ycie wsp�czynnika czu�o�ci myszy

        yRotation += mouseX;                                                        //dodanie warto�ci zapisanej w zmiennej mouseX do warto�ci zapisanie w zmiennej yRotation
        xRotation -= mouseY;                                                        //odejmuje warto�� zmiennej mouseY od warto�cio w zmiennej xRotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);                              //ograniczenie zakresu zmiennej xRotation mi�dzy -90 a 90 stopni

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);             //zamiana obrotu Transform.rotation za pomoc� warto�ci xRotation i yRotation == pozwala na obr�t tylko w osi x i y a z ustawia na 0
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);                   //zamienia obr�t obiektu orientacji za pomoc� yRotation i ustawia tylko obr�t wok� osi y, ustawiaj�c obr�t x i z na 0
    }
}