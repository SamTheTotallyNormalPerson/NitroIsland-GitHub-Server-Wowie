using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentWumps;
    public int oldWumps;
    public Text wumpatext;
    public int currentBox;
    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddWumpa(int wumptoadd)
    {
        currentWumps += wumptoadd;
        wumpatext.text = "" + currentWumps;
        anim.SetBool("WumpAdd", true);
        Invoke("jeff", 1f);
    }

    public void AddBox (int boxtoadd)
    {
        currentBox += boxtoadd;
    }

    public void jeff()
    {
        anim.SetBool("WumpAdd", false);
}

}
