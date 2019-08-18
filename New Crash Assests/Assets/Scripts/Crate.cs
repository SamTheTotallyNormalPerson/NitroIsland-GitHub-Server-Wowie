using System.Collections;
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
    public AudioSource TntSound;
    public GameObject BounceBox;
    public GameObject CrateEffect;

    // Use this for initialization
    void Start () {

        BounceBox.SetActive(false);

        NitroBlowUp.SetActive(false);

        CrateEffect.SetActive(false);

        if (CrateHealth == 0)
        {
            FindObjectOfType<GameManager>().AddBox(CrateVaule);
            Destroy(gameObject);
            
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (IsRegular == true && CrateHealth == 0)
        {
            
            FindObjectOfType<GameManager>().AddBox(CrateVaule);
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            Destroy(gameObject, destoryTime);
            BounceBox.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            CrateEffect.SetActive(true);
        }

        if (IsNitro == true && CrateHealth == 0)
        {
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            NitroBlowUp.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            Destroy(gameObject, destoryTime);
        }

        if (IsAkuAku == true && CrateHealth == 0)
        {
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            Instantiate(masky, transform.position, transform.rotation);
            Destroy(gameObject, destoryTime);
            BounceBox.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        if (IsTnt == true && CrateHealth == 0)
        {
            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            NitroBlowUp.SetActive(true);
            gameObject.GetComponent<Rigidbody>().useGravity = false;

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spin" && IsRegular == true)
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }

        if (other.tag == "Player" && IsNitro == true)
        {
            CrateHealth -= 1;
            
        }

       if (other.tag == "Spin" && IsTnt == true)
        {
            Invoke("OnTntEnter", 4.5f);
            TntSound.Play();
        }

        if (other.tag == "Spin" && IsNitro == true)
        {
            CrateHealth -= 1;
        }

        else 


        if (other.tag == "Jump")
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }

       if (other.tag == "Death")
        {
            CrateHealth -= 1;
        }
    }

    void OnTntEnter()
    {
        CrateHealth -= 1;
    }
   
}

    
