#pragma strict
//This Script will allow Crash to kill enemies or destroy certain objects

var Enemy : GameObject;
var AfterKill : GameObject;

function OnTriggerEnter(other : Collider)

{
 	 if(other.gameObject.tag == "Spin")
  		{
  			//Destroy(object);
   	 		Enemy.SetActive (false);
    		AfterKill.SetActive (true);
  		}
}
