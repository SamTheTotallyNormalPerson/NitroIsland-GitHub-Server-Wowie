#pragma strict

var Bees : GameObject;

function OnTriggerEnter(other : Collider)
{
  if(other.gameObject.tag == "Spin")
  {
     Bees.SetActive (true);
  }
}