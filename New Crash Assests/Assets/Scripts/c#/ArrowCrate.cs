using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCrate : MonoBehaviour
{
    public float CrateHealth;

    public int CrateVaule;

    public AudioSource crateDamnage;

    public float destoryTime;

    public GameObject CrateMesh;

    public BoxCollider cratecollider;

    public GameObject BounceBox;

    public GameObject CrateEffect;

    public Animator anim;
    // Use this for initialization
    void Start()
    {
       

        CrateEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CrateHealth <= 0)
        {

            CrateMesh.SetActive(false);
            Destroy(cratecollider);
            Destroy(gameObject, destoryTime);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            CrateEffect.SetActive(true);
            BounceBox.SetActive(true);
            Destroy(BounceBox, .5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spin" || other.tag == "Flop")
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }

        if (other.tag == "Jump")
        {
            
            anim.SetBool("Bounce", true);
        }

       if(other.tag !="Jump") 
        {
            
            anim.SetBool("Bounce", false);
        }

    }
}
