using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter (Collider col)
    {
       if(col.gameObject.tag == "Player")
        {
            Vector3 hitDirection = col.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);
        }
    }
}
