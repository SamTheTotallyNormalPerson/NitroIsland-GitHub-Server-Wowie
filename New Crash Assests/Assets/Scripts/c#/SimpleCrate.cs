using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCrate : MonoBehaviour
{
    public float CrateHealth;
    public int CrateVaule;
    public GameObject CrateMesh;
    public BoxCollider cratecollider;
    public float destoryTime;
    public GameObject CrateEffect;
    public GameObject BounceBox;
    public AudioSource crateDamnage;
    // Use this for initialization
    void Start()
    {
    }

        // Update is called once per frame
        void Update()
        {
        if (CrateHealth == 0)
        {
            FindObjectOfType<GameManager>().AddBox(CrateVaule);
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
        if (other.tag == "Spin" || other.tag == "Jump")
        {
            CrateHealth -= 1;
            crateDamnage.Play();

        }


        else

       if (other.tag == "Death")
        {
            CrateHealth -= 1;
        }

        if (other.tag == "Flop")
        {
            CrateHealth -= 1;
        }
    }

}

