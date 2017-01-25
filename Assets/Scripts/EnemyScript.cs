using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {


	public Person personScript;
	public bool turn;

	public GameObject North;
	public GameObject East;
	public GameObject South;
	public GameObject West;

	void Start () {
		turn = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool turn = GameObject.Find ("Player").GetComponent<Player>().turnTaken;

		if(turn){
//			Debug.Log ("BadBoys turn");
			int dir = Random.Range (0, 5);
			if(dir == 0){
				if (!West.GetComponent<NextBlock>().damage)
					personScript.moveLeft ();
				//Debug.Log ("Bad boy left " + dir);
			}
			else if(dir == 1){
				if (!East.GetComponent<NextBlock>().damage)
					personScript.moveRight ();
				//Debug.Log ("Bad boy right " + dir);
			}
			else if(dir == 2){
				if (!North.GetComponent<NextBlock>().damage)
					personScript.moveUp ();
				//Debug.Log ("Bad boy up " + dir);
			}
			else if(dir == 3){
				if (!South.GetComponent<NextBlock>().damage) 
					personScript.moveDown ();
				//Debug.Log ("Bad boy down " + dir);
			}
			else{
				personScript.attack ();
				//Debug.Log ("Bad boy attack :( " + dir);
			}
			turn = false;
		}

	}

	public void turnNow(){
		turn = true;
	}
}
