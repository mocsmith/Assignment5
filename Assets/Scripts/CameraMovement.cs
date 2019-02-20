using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Transform player2;
    //private Vector3 offset;
    //private bool camera1;

    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    void Update()
    { 
        /*if (Input.GetKey(KeyCode.C))
        {
            camera1 = true;
        }
        else if(Input.GetKey(KeyCode.V))
        {
            camera1 = false;
        }*/
    }
    void LateUpdate()
    {
        Vector3 cameraUpdate1;
        Vector3 cameraUpdate2;

        if (player.transform.position.x < -20f && player.transform.position.z > -20f)
        {
            cameraUpdate1 = transform.position;
            //cameraUpdate2 = transform.eulerAngles;
            cameraUpdate1.x = -42.5f;
            cameraUpdate1.y = 7.5f;
            cameraUpdate1.z = 2.5f;
            transform.position = cameraUpdate1;
            transform.LookAt(player2);
        }
        else if (player.transform.position.x < -20f && player.transform.position.z < -20f)
        {
            cameraUpdate1 = transform.position;
            cameraUpdate2 = transform.eulerAngles;
            //transform.position = player.transform.position;
            //cameraUpdate2 = transform.eulerAngles;
            cameraUpdate1.x = -32.5f;
            cameraUpdate1.y = 25f;
            cameraUpdate1.z = -32.5f;
            transform.position = cameraUpdate1;
            cameraUpdate2.x = 90f;
            cameraUpdate2.y = 180f;
            cameraUpdate2.z = 0f;
            transform.eulerAngles = cameraUpdate2;
        }
        else
        {
            transform.position = player.transform.position;// + offset;
            cameraUpdate1 = transform.position;
            cameraUpdate2 = transform.eulerAngles;
            cameraUpdate1.x = -7.5f;
            cameraUpdate1.y = 12.5f;
            //cameraUpdate1.z = cameraUpdate1.z + 10;
            transform.position = cameraUpdate1;
            cameraUpdate2.x = 90f;
            cameraUpdate2.y = 0f;
            cameraUpdate2.z = 0f;
            transform.eulerAngles = cameraUpdate2;
        }

        /*if (camera1)
        {
            transform.position = player.transform.position + offset;
            cameraUpdate1 = transform.position;
            cameraUpdate2 = transform.eulerAngles;
            cameraUpdate1.y = 10f;
            cameraUpdate1.z = cameraUpdate1.z + 10;
            transform.position = cameraUpdate1;
            cameraUpdate2.x = 0f;
            transform.eulerAngles = cameraUpdate2;
        }
        else
        {
            transform.position = player.transform.position + offset;
            cameraUpdate1 = transform.position;
            cameraUpdate2 = transform.eulerAngles;
            cameraUpdate1.y = 10f;
            if(!camera1)
            {
                cameraUpdate1.z = player.transform.position.z - 10;
            }
            transform.position = cameraUpdate1;
            cameraUpdate2.x = 90f;
            transform.eulerAngles = cameraUpdate2;
        }*/
    }
}
