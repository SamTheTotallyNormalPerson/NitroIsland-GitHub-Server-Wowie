using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash : MonoBehaviour
{

    public float moveSpeed;
    public CharacterController controller;
    public float jumpForce;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;

    public bool isCrouching;

    public GameObject PlayerModel;

    public GameObject SpinObject;

    public AudioSource jumpSound;

    public GameObject jumpBox;

    public GameObject Mask;

    public GameObject MaskSpawn;

    public AudioSource Wine;

    public bool isAlive;

    public AudioSource ded;

    public float KnockBackForce;
    public float KnockBackTime;
    private float KnockBackCounter;

    public float characterCrouchPos;

    public HealthManager health;

    public bool isFlop;

    public bool isJump;

    public GameObject FlopCollider;

    public bool isRun;

    public bool isCrouch;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();

        health = FindObjectOfType<HealthManager>();
        SpinObject.SetActive(false);

        //  jumpBox.SetActive(false);

        isAlive = true;

        FlopCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        {


            // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
            if (KnockBackCounter < -0)
            {

                    

                float yStore = moveDirection.y;

                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

                moveDirection = moveDirection.normalized * moveSpeed;
                moveDirection.y = yStore;

                //Jump

                if (controller.isGrounded)
                {

                    moveDirection.y = 0f;
                    jumpBox.SetActive(false);


                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection.y = jumpForce;
                        jumpSound.Play();
                        jumpBox.SetActive(true);
                        isJump = true;

                    }

                    else
                    {
                        isJump = false;
                    }
                }

                // BellyFlop

                if (!controller.isGrounded && Input.GetButton("Fire1") && isFlop == false && isJump == true)
                {
                    Invoke("Flop", .5f);
                    isAlive = false;
                    anim.SetBool("isFlop", true);
                    isFlop = true;
                    FlopCollider.SetActive(false);
                }


                if (controller.isGrounded && isFlop == true && isJump == false)
                {
                    Invoke("Pause", 1.5f);
                    gravityScale = 0.05f;
                    anim.SetBool("isFlop", false);
                    isFlop = false;
                    isAlive = false;
                    FlopCollider.SetActive(true);
                }
                // Crouch

                if (controller.isGrounded && Input.GetButton("Fire1") && isRun == false)
                {
                    Crouch();
                    isCrouch = true;

                }

                if (controller.isGrounded && !Input.GetButton("Fire1"))
                {
                    anim.SetBool("isDown", false);
                    moveSpeed = 4f;
                    controller.height = 0.85f;
                    isCrouch = false;
                }

                // Slide

               if (isRun == true && Input.GetButtonDown("Fire1") && isCrouch == false)
                {
                    Debug.Log("I am Sliding into the D.Ms");
                    Invoke("Norm", 1f);
                    moveSpeed = 7;
                }


                // Spin

                if (Input.GetButtonDown("Spin"))
                {
                    {
                        Invoke("Sppin", .5f);
                        SpinObject.SetActive(true);
                        PlayerModel.SetActive(false);
                        moveSpeed = 6f;

                    }



                }



            }
            else
            {
                KnockBackCounter -= Time.deltaTime;
            }

            //

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
            controller.Move(moveDirection * Time.deltaTime);

            //Move The Player In Diffrent Directions Base On The Camera Look

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            //Animations
            anim.SetBool("isGrounded", controller.isGrounded);
            anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));


            if (Input.GetAxis("Vertical") > 0 || (Input.GetAxis("Horizontal") > 0))   
            {
                isRun = true;
            }

            else
            {
                isRun = false; 
            }

            if (!controller.isGrounded)
            {
                jumpBox.SetActive(true);
            }









        }
    }

    public void KnockBack(Vector3 direction)
    {
        KnockBackCounter = KnockBackTime;


        moveDirection = direction * KnockBackForce;
        moveDirection.y = KnockBackForce;


    }
    void Sppin()
    {
        SpinObject.SetActive(false);
        PlayerModel.SetActive(true);
        moveSpeed = 8f;

    }


    void Jumper()
    {
        jumpBox.SetActive(true);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            Wine.Play();


        }

        if (other.CompareTag ("Bouncey"))
        {
            moveDirection.y = jumpForce;
            //  jumpSound.Play();
            jumpBox.SetActive(true);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Crate")
        {
            moveDirection.y = jumpForce;
            // jumpSound.Play();
            jumpBox.SetActive(true);
        }
    }

    void Crouch()
    {
        anim.SetBool("isDown", true);
        moveSpeed = 2f;
        controller.height = .5f;
        
    }

    void Flop()
    {
        isAlive = true;
        gravityScale = .25f;
        FlopCollider.SetActive(false);

    }

    void Pause()
    {
        isAlive = true;
        FlopCollider.SetActive(false);
    }

    void Norm()
    {
        moveSpeed = 4;
    }
}
