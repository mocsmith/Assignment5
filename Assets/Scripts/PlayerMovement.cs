using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    //public Text countText;
    //public Text successText;
    private Rigidbody rb;
    //private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*count = 0;
        SetCountText();
        successText.text = "";*/
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (transform.position.x < -20f && transform.position.z > -10f)
        {
            Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
            rb.AddForce(movement * speed);
        }
        else if (transform.position.x < -20f && transform.position.z <= -10f)
        {
            Vector3 movement = new Vector3(-moveHorizontal, 0.0f, -moveVertical);
            rb.AddForce(movement*speed);
        }
        else
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }*/

    /*void SetCountText()
    {
        countText.text = "Collectibles: " + count.ToString();
        if (count == 3)
        {
            successText.text = "You Did It!";
        }
    }*/
}
