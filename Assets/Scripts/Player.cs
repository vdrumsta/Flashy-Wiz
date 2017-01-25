using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//public Vector3 pos; 
	public bool turnTaken;
	public Person personScript;

	public GameObject North;
	public GameObject East;
	public GameObject South;
	public GameObject West;
	public GameObject Hit;
	public GameObject Slots;
	public string abilities;
	public string [] selectedAbilities;
	private int turnsExecuted;

	private bool readyToMoveLeft;
	private bool readyToMoveRight;
	private bool readyToMoveUp;
	private bool readyToMoveDown;

	// Use this for initialization
	void Start () {
		turnTaken = false;
		turnsExecuted = 9001;
		selectedAbilities = new string[3];
	}
	
	// Update is called once per frame
	void Update () {
		if (turnsExecuted < 3) {
			abilities = selectedAbilities[turnsExecuted];
			if (abilities == "Left") {
				if (West.GetComponent<NextBlock> ().door && gameObject.GetComponent<PlayerHit> ().returnKeys () > 0 || !West.GetComponent<NextBlock> ().door) {
					Invoke ("MoveLeft", 0.5f * turnsExecuted);
				}
			} else if (abilities == "Right") {
				if (East.GetComponent<NextBlock> ().door && gameObject.GetComponent<PlayerHit> ().returnKeys () > 0 || !East.GetComponent<NextBlock> ().door) {
					Invoke ("MoveRight", 0.5f * turnsExecuted);
				}
			}
			//			up/down movement
			else if (abilities == "Up") {
				if ((North.GetComponent<NextBlock> ().returnDoor () && Hit.GetComponent<PlayerHit> ().returnKeys () > 0) || !North.GetComponent<NextBlock> ().returnDoor ()) {
					Invoke ("MoveUp", 0.5f * turnsExecuted);
				}
			} else if (abilities == "Down") {
				if (South.GetComponent<NextBlock> ().door && gameObject.GetComponent<PlayerHit> ().returnKeys () > 0 || !South.GetComponent<NextBlock> ().door) {
					Invoke ("MoveDown", 0.5f * turnsExecuted);
				}
			}
			//			attacking
			else if (abilities == "Attack") {
				personScript.attack ();
			}
			
			turnsExecuted += 1;
		}

		if (readyToMoveLeft) {
			personScript.moveLeft ();
			turnTaken = true;
			readyToMoveLeft = false;
		} else if (readyToMoveRight) {
			personScript.moveRight ();
			turnTaken = true;
			readyToMoveRight = false;
		} else if (readyToMoveUp) {
			personScript.moveUp ();
			turnTaken = true;
			readyToMoveUp = false;
		} else if (readyToMoveDown) {
			personScript.moveDown ();
			turnTaken = true;
			readyToMoveDown = false;
		}
	}

	void LateUpdate () {
		turnTaken = false;
	}

	public void ReceiveAbilities (string[] rolledAbilities) {
		selectedAbilities = rolledAbilities;
		turnsExecuted = 0;
	}

	private void MoveLeft () {
		readyToMoveLeft = true;
	}

	private void MoveRight () {
		readyToMoveRight = true;
	}

	private void MoveUp () {
		readyToMoveUp = true;
	}

	private void MoveDown () {
		readyToMoveDown = true;
	}
}
