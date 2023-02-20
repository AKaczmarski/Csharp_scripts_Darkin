using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]                               //sekcja Sway Settings
    [SerializeField] private float smooth;                  // dwie zmienne smooth i swayMultiplier
    [SerializeField] private float swayMultiplier;          //zmienne s¹ oznaczone atrybutem SerializeField

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;        //dane wejœciowe z myszy dla osi X => Kontrola iloœci ko³ysania w grze
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;        //dane wejœciowe z myszy dla osi Y 
                                                                            //Sterowanie ruchem kamery w grze w zale¿noœci od ruchu myszy

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);//obrót dla osi X jest okreœlony przez k¹t -mouseY i oœ obrotu Wektor3 w prawo
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);    //obrót dla osi Y jest okreœlony przez k¹t mouseX i oœ obrotu Wektor3 w górê

        Quaternion targetRotation = rotationX * rotationY;                  //obrót w kolejnoœci obrótX a nastêpnie obrótY

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        //lokalna roatacja komponentu Transform obiektu gameObject na wyg³adzon¹ interpolacjê miêdzy jego aktualn¹ rotacj¹ a rotacj¹ do której d¹¿y przy u¿yciu metody Slerp
        //dziêki temu jest p³ynna animacja gdy rêka pod¹¿a za kamer¹
    }
}
