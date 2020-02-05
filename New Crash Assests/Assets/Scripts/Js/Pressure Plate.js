#pragma strict

var ButtonUnpushed : GameObject;
var ButtonPushed : GameObject;


//When Crash Enters the Collider. It will activate the pushed Button
function OnTriggerStay(other : Collider)
	{
  		if(other.gameObject.tag == "Player")
  		{
  			ButtonUnpushed.SetActive(false);
  			ButtonPushed.SetActive(true);
  		}
  	}

//When Crash leaves the Collider. It will deactivate the pushed button
function OnTriggerExit(other : Collider)
	{
  		if(other.gameObject.tag == "Player")
  		{
  			ButtonUnpushed.SetActive(true);
  			ButtonPushed.SetActive(false);
  		}
  	}