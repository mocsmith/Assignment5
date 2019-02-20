using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    private Light pLight;
    float lightC;

    void Start()
    {
        pLight = GetComponent<Light>();
        pLight.enabled = false;
        lightC = 1;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            pLight.enabled = !pLight.enabled;

            if (lightC == 1)
            {
                pLight.color = new Color(255, 0, 0);
                lightC = 2;
            }
            else if (lightC == 2)
            {
                pLight.color = new Color(0, 0, 255);
                lightC = 3;
            }
            else if (lightC == 3)
            {
                pLight.color = new Color(0, 255, 0);
                lightC = 1;
            }
        }
    }
}
