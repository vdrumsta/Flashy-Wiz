using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public GameObject player;
	public GameObject doorSprite;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			//if (player.GetComponentsInChildren<PlayerHit>().keyCount > 0){
			if(player.GetComponent<PlayerHit>().returnKeys() > 0){
				transform.gameObject.tag = "Ground";
				doorSprite.SetActive (false);
			}
		}
	}
}
