﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public float CrateHealth;
    public int CrateVaule;
    public AudioSource crateDamnage;
    public float destoryTime;
    public GameObject CrateMesh;
    public GameObject NitroBlowUp;
    public BoxCollider cratecollider;
    public GameObject masky;

    public bool IsNitro;
    public bool IsAkuAku;
    public bool IsTnt;
    public bool IsRegular;
    public bool IsBounce;

    public AudioSource TntSound;
    public GameObject BounceBox;
    public GameObject CrateEffect;

    // Use this for initialization
    void Start () {

        BounceBox.SetActive(false);

        NitroBlowUp.SetActive(false);

        CrateEffect.SetActive(false);

        if (CrateHealth <= 0)
        {
            FindObjectOfType<GameManager>().AddBox(CrateVaule);
            Destroy(gameObject);
            
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (IsRegular == true && CrateHealth <= 0)
        {
          
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            Destroy(gameObject, destoryTime);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            CrateEffect.SetActive(true);
            BounceBox.SetActive(true);
            Destroy(BounceBox, .5f);
        }

        if (IsNitro == true && CrateHealth <= 0)
        {
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            NitroBlowUp.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            Destroy(gameObject, destoryTime);
        }

        if (IsAkuAku == true && CrateHealth <= 0)
        {
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            Instantiate(masky, transform.position, transform.rotation);
            Destroy(gameObject, destoryTime);
            BounceBox.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        if (IsTnt == true && CrateHealth <= 0)
        {
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            NitroBlowUp.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            Destroy(gameObject, destoryTime);
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spin" || other.tag == "Jump" && IsRegular == true)
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }

        if (other.tag == "Player" && IsNitro == true)
        {
            CrateHealth -= 1;
            
        }

       if (other.tag == "Jump" && IsTnt == true)
        {
            Invoke("OnTntEnter", 4);
            TntSound.Play();
        }

        if (other.tag == "Spin" && IsNitro == true || IsTnt == true)
        {
            CrateHealth -= 1;
        }

       if (other.tag == "Death")
        {
            CrateHealth -= 1;
        }

       if (other.tag == "Flop")
        {
            CrateHealth -= 1;
        }

       if (other.tag == "Jump" && IsBounce == true)
        {
            BounceBox.SetActive(true);
        }

        if (other.tag != "Jump" && IsBounce == true)
        {
            BounceBox.SetActive(false);
        }
    }

    void OnTntEnter()
    {
        CrateHealth -= 1;
    }
   
    void OnBounceBoxEnter()
    {
        BounceBox.SetActive(true);
    }
}

    
