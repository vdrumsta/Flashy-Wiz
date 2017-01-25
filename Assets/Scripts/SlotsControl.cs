using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SlotsControl : MonoBehaviour {

	private string[] selectedAbilities = new string[3];
	public GameObject[] columns = new GameObject[3];
	private int columnCounter;
	public bool slotFinish;
	public bool readyToStop;
	public float spinSpeed;
	public Player playerScript;

	private AudioSource audio;

	public GameObject ball;
	private Animator ballAnimator;

	// Use this for initialization
	void Start () {
		readyToStop = false;
		slotFinish = false;
		Invoke("StartSpinning", 1f);
		ballAnimator = ball.GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (readyToStop && Input.GetKeyDown ("space") && columnCounter < columns.Length && !slotFinish) {
			selectedAbilities[columnCounter] = columns[columnCounter].GetComponent<ColumnControl> ().StopColumn();
			columnCounter += 1;

			audio.pitch = columnCounter;
			audio.Play();

			ballAnimator.SetBool ("down", true);
			if (columnCounter < columns.Length) { // if it's not the last column
				Invoke ("SetDownTrue", 0.1f);
			}

			if (columnCounter == 3) {
				// execute abilities
				readyToStop = false;
				slotFinish = true;
				playerScript.ReceiveAbilities (selectedAbilities);
				//Debug.Log ("Executed Abilities: " + selectedAbilities [0] + "-->" + selectedAbilities [1] + "-->" + selectedAbilities [2]);
				columnCounter = 0;
				Invoke ("StartSpinning", 2f);
			}
		}
	}

//	public string slotsLanded(){
//		return selectedAbilities;
//	}

	public void setSlotFinishFalse(){
		slotFinish = false;
	}

	public void StartSpinning () {
		slotFinish = false;
		for (int i = 0; i < columns.Length; i++) {
			columns [i].GetComponent<ColumnControl> ().StartColumn (spinSpeed);
		}
		Invoke ("SetDownTrue", 0.1f);
	}

	private void SetDownTrue () {
		ballAnimator.SetBool ("down", false);
	}
}
