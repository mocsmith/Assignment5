using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    float speed = 6;
    float gravity = 8;
    public int count = 0;

    Vector3 moveDir = new Vector3(0, 0, 0);

    CharacterController controller;
    Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("condition", 1);
                if (transform.position.x < -20f && transform.position.z > -20f)
                {
                    moveDir = new Vector3(1, 0, 0);
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else if (transform.position.x < -20f && transform.position.z <= -20f)
                {
                    moveDir = new Vector3(0, 0, -1);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    moveDir = new Vector3(0, 0, 1);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("condition", 1);
                if (transform.position.x < -20f && transform.position.z > -20f)
                {
                    moveDir = new Vector3(0, 0, 1);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.position.x < -20f && transform.position.z <= -20f)
                {
                    moveDir = new Vector3(1, 0, 0);
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else
                {
                    moveDir = new Vector3(-1, 0, 0);
                    transform.eulerAngles = new Vector3(0, 270, 0);
                }
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetInteger("condition", 1);
                if (transform.position.x < -20f && transform.position.z > -20f)
                {
                    moveDir = new Vector3(-1, 0, 0);
                    transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else if (transform.position.x < -20f && transform.position.z <= -20f)
                {
                    moveDir = new Vector3(0, 0, 1);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    moveDir = new Vector3(0, 0, -1);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("condition", 1);
                if (transform.position.x < -20f && transform.position.z > -20f)
                {
                    moveDir = new Vector3(0, 0, -1);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (transform.position.x < -20f && transform.position.z <= -20f)
                {
                    moveDir = new Vector3(-1, 0, 0);
                    transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else
                {
                    moveDir = new Vector3(1, 0, 0);
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }
                moveDir *= speed;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            ParticleSystem collect = other.gameObject.GetComponent<ParticleSystem>();
            collect.Play();
            //other.gameObject.SetActive(false);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            count = count + 1;
        }
    }
}
