using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{

    public SoundsInGame sounds4;

    void Update()
    {
        //Jeœli gracz wciœnie W lub S lub A lub D lub W i A lub W i D
        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D))))
        {
            sounds4.footstepsSound();       //dzwiek

            //jeœli gracz puœci W lub S lub A lub D lub W i A lub W i D
            if ((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.W)) && (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.W)) && (Input.GetKeyUp(KeyCode.D))))
            {
                sounds4.footOffSound();     //wy³¹czanie dzwieku
            }
        }
    }
}
