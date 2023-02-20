using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]                               //sekcja Sway Settings
    [SerializeField] private float smooth;                  // dwie zmienne smooth i swayMultiplier
    [SerializeField] private float swayMultiplier;          //zmienne s� oznaczone atrybutem SerializeField

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;        //dane wej�ciowe z myszy dla osi X => Kontrola ilo�ci ko�ysania w grze
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;        //dane wej�ciowe z myszy dla osi Y 
                                                                            //Sterowanie ruchem kamery w grze w zale�no�ci od ruchu myszy

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);//obr�t dla osi X jest okre�lony przez k�t -mouseY i o� obrotu Wektor3 w prawo
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);    //obr�t dla osi Y jest okre�lony przez k�t mouseX i o� obrotu Wektor3 w g�r�

        Quaternion targetRotation = rotationX * rotationY;                  //obr�t w kolejno�ci obr�tX a nast�pnie obr�tY

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        //lokalna roatacja komponentu Transform obiektu gameObject na wyg�adzon� interpolacj� mi�dzy jego aktualn� rotacj� a rotacj� do kt�rej d��y przy u�yciu metody Slerp
        //dzi�ki temu jest p�ynna animacja gdy r�ka pod��a za kamer�
    }
}
