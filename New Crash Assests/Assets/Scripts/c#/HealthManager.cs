using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;

    public GameObject AkuAku;

    public Crash theplayer;

    public float invincibilityLength;
    private float invinciblityCounter;

    public Renderer crashRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;


    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public AudioSource Woah;

    // Use this for initialization
    void Start () {

        currentHealth = 1;

        AkuAku.SetActive(false);

        respawnPoint = theplayer.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth > 1)
        {
            AkuAku.SetActive(true);
        } else
        {
            AkuAku.SetActive(false);
        }

        if (invinciblityCounter > 0)
        {
            invinciblityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                crashRenderer.enabled = !crashRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityLength <= 0)
            {
                crashRenderer.enabled = true;
            }
        }
	}

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invinciblityCounter <= 0)
        {


            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
                Woah.Play();
                
            }
            else
            {
                theplayer.KnockBack(direction);

                invinciblityCounter = invincibilityLength;

                crashRenderer.enabled = false;

                flashCounter = flashLength;

            }
        }
    }

    public void Respawn()
    {
        // theplayer.transform.position = respawnPoint;
        //  currentHealth = 1;
        if (!isRespawning)
        {


            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        theplayer.gameObject.SetActive(false);


        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;

        theplayer.gameObject.SetActive(true);
        theplayer.transform.position = respawnPoint;
        currentHealth = 1;

        invinciblityCounter = invincibilityLength;

        crashRenderer.enabled = false;

        flashCounter = flashLength;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
    }
}
