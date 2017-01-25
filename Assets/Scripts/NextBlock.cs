using UnityEngine;
using System.Collections;

public class NextBlock : MonoBehaviour {

	public bool wall;
	public bool door;
	public bool damage;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy") {
			wall = true;
		} 
		else if (col.gameObject.tag == "Ground") {
			wall = false;
			damage = false;
			door = false;
		}
		else if (col.gameObject.tag == "Damage") {
			damage = true;
		} 
		else if (col.gameObject.tag == "Door") {
			door = true;
			wall = false;
		} 
	}

	public bool returnDoor(){
		return door;
	}
	
}
