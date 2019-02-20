using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject door2;
    public int doorCount;
    Vector3 doorUpdate;
    Vector3 door2Update;

    // Update is called once per frame
    void Update()
    {
        doorCount = GameObject.Find("Player").GetComponent<KnightController>().count;
        doorUpdate = door.transform.position;
        door2Update = door2.transform.position;

        if (doorCount >= 3 && door.transform.position.z < 12)
        {
            doorUpdate.z = doorUpdate.z + 0.05f;
            door.transform.position = doorUpdate;
        }
        if (doorCount >= 6 && door2.transform.position.x < -15)
        {
            door2Update.x = door2Update.x - 0.05f;
            door2.transform.position = door2Update;
        }
    }
}
