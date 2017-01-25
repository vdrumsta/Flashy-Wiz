using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Hit"){
			gameObject.SetActive (false);
		}
	}
}
