using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitforFade;

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

        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.b, blackScreen.color.g, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.b, blackScreen.color.g, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
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


        isFadeToBlack = true;

        yield return new WaitForSeconds(waitforFade);
        isRespawning = false;

        isFadeFromBlack = true;

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
