using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningItem : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 0f, 70f * Time.deltaTime, Space.Self);
        //obracanie wok� w�asnej osi Z o 70 stopni na sekund�
    }
}
