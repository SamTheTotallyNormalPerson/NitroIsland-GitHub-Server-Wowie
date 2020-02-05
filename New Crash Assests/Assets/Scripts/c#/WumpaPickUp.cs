using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WumpaPickUp : MonoBehaviour {

    public int value;
    public AudioSource wumpapickyupper;
    public float destoryTime = .12f;
    public GameObject wumpaMesh;
    public float lookRadius = 10f;

   public Transform target;
    public float speed;
	// Use this for initialization
	void Start () {

        
       
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wumpapickyupper.Play();
            FindObjectOfType<GameManager>().AddWumpa(value);
            FindObjectOfType<GameManager>().anim.SetBool("WumpAdd", true);
            FindObjectOfType<GameManager>().Invoke("jeff", 1f);
            Destroy(wumpaMesh);
            Destroy(gameObject , destoryTime);
            
        }
    }

   void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
