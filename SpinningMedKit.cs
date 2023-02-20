using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningMedKit : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f,  70f * Time.deltaTime, 0f, Space.Self);
        //obracanie gameObjectu o 70 stopni na sekundê wokó³ osi y
    }
}
