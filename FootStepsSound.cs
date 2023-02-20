using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{

    public SoundsInGame sounds4;

    void Update()
    {
        //Je�li gracz wci�nie W lub S lub A lub D lub W i A lub W i D
        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D))))
        {
            sounds4.footstepsSound();       //dzwiek

            //je�li gracz pu�ci W lub S lub A lub D lub W i A lub W i D
            if ((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.W)) && (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.W)) && (Input.GetKeyUp(KeyCode.D))))
            {
                sounds4.footOffSound();     //wy��czanie dzwieku
            }
        }
    }
}
