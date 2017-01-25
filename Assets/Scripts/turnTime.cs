using UnityEngine;
using System.Collections;

public class turnTime : MonoBehaviour {

	public GameObject Player;
	public GameObject Slots;
	public GameObject Hit;
	public GameObject[] enemyList;

	// Use this for initialization
	void Start () {
	}
		
	void FixedUpdate (){
		if (!Hit.GetComponent<PlayerHit> ().dead) {
			if (GameObject.Find ("Player").GetComponent<Player> ().turnTaken) {
				for (int i = 0; i < enemyList.Length; i++) {
					GameObject enemy = enemyList [i];
					enemy.GetComponent<EnemyScript> ().turnNow ();
				}
				GameObject.Find ("Player").GetComponent<Player> ().turnTaken = false;
				Slots.GetComponent<SlotsControl> ().slotFinish = false;
				Debug.Log ("Back to player turn + " + GameObject.Find ("Player").GetComponent<Player> ().turnTaken);
			}
		}
	}
}
