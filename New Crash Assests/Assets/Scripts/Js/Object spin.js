#pragma strict

var x : int = 0;
var y : int = 0;
var z : int = 0;


function Update()
{
	transform.Rotate(x, y, z);
}