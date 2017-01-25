using UnityEngine;
using System.Collections;

public class ColumnControl : MonoBehaviour {

	public GameObject availableAbilities;						// the parent of all available abilities
	private GameObject[] availableAbilitiesArray;				// available abilities that can be spawned
	private float[] availableAbilitiesPercentages;				// each ability has a certain chance to spawn
	public GameObject[] visibleAbilities = new GameObject[4];	// the abilities that are currently scrolling
	private Vector3 abilitySpawnPos;							// position where the abilities spawn if there's an error
	private bool startingSpin;
	public SlotsControl slotsScript;
	public float scrollSpeed;

	private int snapCycles;
	private int selectedAbilityIndex;
	private Vector3 snapPos;

	private int currentCosMovementCycle;
	private int totalCosMovementCycles;

	public GameObject triangle;
	public GameObject triangle2;
	public Sprite triangleOff;
	public Sprite triangleOn;

	// Use this for initialization
	void Start () {
		startingSpin = true;
		snapCycles = 0;
		snapPos = visibleAbilities [0].transform.position;
		snapPos.y = triangle.transform.position.y;

		currentCosMovementCycle = 0;
		totalCosMovementCycles = -1;

		abilitySpawnPos = visibleAbilities [0].transform.position;	// the top most visible ability position is recorded

		availableAbilitiesArray = new GameObject[availableAbilities.transform.childCount];		// length of array equals the number of available abilities
		availableAbilitiesPercentages = new float[availableAbilitiesArray.Length];
		for (int i = 0; i < availableAbilitiesArray.Length; i++) {								// stores available abilities into an array and their percentages to spawn into a diff array
			availableAbilitiesArray[i] = availableAbilities.transform.GetChild(i).gameObject;

			availableAbilitiesPercentages [i] = availableAbilitiesArray [i].GetComponent<AbilityScript> ().spawnChance;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < visibleAbilities.Length; i++) {
			if (visibleAbilities [i] == null) { // checks if any of the abilities is destroyed (which happens when it moves below the camera). if it is, then spawns a new one
				if (startingSpin && i == 0) {
					startingSpin = false;
					slotsScript.readyToStop = true;  // Allows to stop slots when the original highest empty block gets destroyed
				}
				SpawnAbility (i);

				// figures out the index of the current highest ability
				int indexOfHighestAbility = i + 1;
				if (indexOfHighestAbility == visibleAbilities.Length) { // if index is out of bounds set it to the last index
					indexOfHighestAbility = 0;
				}
					
				if (visibleAbilities [indexOfHighestAbility] == null) {			// default case
					visibleAbilities [i].transform.position = abilitySpawnPos;	// this is the default position, but only used if the highest ability is missing (could be caused by setting spin speed too high)
				} else {
					visibleAbilities [i].transform.position = visibleAbilities [indexOfHighestAbility].transform.position + new Vector3 (0, +3.35f, 0); // new ability is placed above the highest ability
				}

				visibleAbilities [i].SetActive (true);	// activate the ability
			}

		}

		if (snapCycles > 0) {
			float distanceMoved = visibleAbilities [selectedAbilityIndex].transform.position.y;

			visibleAbilities [selectedAbilityIndex].transform.position = Vector3.Lerp (visibleAbilities [selectedAbilityIndex].transform.position, snapPos, 0.2f);

			distanceMoved -= visibleAbilities [selectedAbilityIndex].transform.position.y;
			for (int i = 0; i < visibleAbilities.Length; i++) {
				if (i != selectedAbilityIndex) {
					visibleAbilities [i].transform.position -= new Vector3 (0, distanceMoved, 0);
				}
			}
			snapCycles -= 1;
		}

		if (currentCosMovementCycle <= totalCosMovementCycles) {
			for (int i = 0; i < visibleAbilities.Length; i++) {
				if (visibleAbilities [i] != null) {
					visibleAbilities [i].GetComponent<AbilityScript> ().scrollSpeed = -scrollSpeed * Mathf.Cos((Mathf.PI / totalCosMovementCycles * currentCosMovementCycle));
				}
			}

			// sets the speed of the abilities that might spawn to 0
			for (int i = 0; i < availableAbilitiesArray.Length; i++) {
				availableAbilitiesArray [i].GetComponent<AbilityScript> ().scrollSpeed = -scrollSpeed * Mathf.Cos((Mathf.PI / totalCosMovementCycles * currentCosMovementCycle));;
			}

			currentCosMovementCycle += 1;
		}
	}

	void SpawnAbility (int spawnIndex) {
		float randomNumber = Random.Range (1f, 100f);	// rolls the percentage
		bool indexFound = false;
		int selectedAbilityIndex = 0;
		float sumUpToIndex = 0;

		for (int i = 0; i < availableAbilitiesPercentages.Length && indexFound == false; i++) {		// checks the percentage of the abilities has been rolled
			sumUpToIndex += availableAbilitiesPercentages [i];

			if (sumUpToIndex > randomNumber) {
				indexFound = true;
				selectedAbilityIndex = i;
			}
		}

		if (indexFound == true) {	// spawns the rolled ability
			visibleAbilities [spawnIndex] = Instantiate (availableAbilitiesArray [selectedAbilityIndex]);	// creates a new ability
		} else {					// default case otherwise which spawns an error

			GameObject error = availableAbilitiesArray [0];
			for (int i = 0; i < availableAbilitiesArray.Length; i++) {
				if (availableAbilitiesArray [i].name == "Error") {
					error = availableAbilitiesArray [i];
				}
			}
			visibleAbilities [spawnIndex] = Instantiate (error);	// creates an error ability
		}
	}

	public string StopColumn () {
		// stops the column spinning
		for (int i = 0; i < visibleAbilities.Length; i++) {
			if (visibleAbilities [i] != null) {
				visibleAbilities [i].GetComponent<AbilityScript> ().scrollSpeed = 0;
			}
		}

		// sets the speed of the abilities that might spawn to 0
		for (int i = 0; i < availableAbilitiesArray.Length; i++) {
			availableAbilitiesArray [i].GetComponent<AbilityScript> ().scrollSpeed = 0;
		}

		// checks the y-coordinates of each ability to determine which one is the closest to the triangle
		float triangleYPos = triangle.transform.position.y;

		float shortestDistance;
		if (visibleAbilities [0]) {
			shortestDistance = System.Math.Abs (visibleAbilities [0].transform.position.y - triangleYPos);	// abs(ability - triangle)
		} else {
			shortestDistance = 9001; // The dankest of the memz
		}
		int closestAbilityIndex = 0; // by default, 0 index is closest
		for (int i = 1; i < visibleAbilities.Length; i++) {	// checks the y-coordinates of each ability to determine which one is the closest to the triangle. Skips 0 as it is the default case
			if (visibleAbilities [i] != null) {
				float abilityYPos = visibleAbilities [i].transform.position.y;
				float currentDistance = System.Math.Abs (abilityYPos - triangleYPos);
//				Debug.Log ("System.Math.Abs(abilityYPos) - System.Math.Abs(triangleYPos): " + System.Math.Abs(abilityYPos) + " - " + System.Math.Abs(triangleYPos));

				if (currentDistance < shortestDistance) {
					shortestDistance = currentDistance;
					closestAbilityIndex = i;
				}
			}
		}

//		Debug.Log ("closestAbilityPos = " + visibleAbilities [closestAbilityIndex].transform.position + "; trianglePos = " + triangle.transform.position + "shortestDistance = " + shortestDistance);

		// changes the sprites of the triangles to make them turn on
		triangle.GetComponent<SpriteRenderer> ().sprite = triangleOn;
		triangle2.GetComponent<SpriteRenderer> ().sprite = triangleOn;

//		Vector3 shortestDistanceVector = new Vector3 (0, visibleAbilities[closestAbilityIndex].transform.position.y - triangleYPos, 0);
//		for (int i = 0; i < visibleAbilities.Length; i++) {	// positions abilities so the selected ability y coordinates is the same as the trianlge y coordinate
//			visibleAbilities [i].transform.position = visibleAbilities [i].transform.position - shortestDistanceVector;
//		}

		snapCycles = 50;
		selectedAbilityIndex = closestAbilityIndex;

		return visibleAbilities [closestAbilityIndex].tag;
	}

	public void StartColumn (float speed) {
		slotsScript.readyToStop = false;
		scrollSpeed = speed;


		// changes the sprites of the triangles to make them turn off
		triangle.GetComponent<SpriteRenderer> ().sprite = triangleOff;
		triangle2.GetComponent<SpriteRenderer> ().sprite = triangleOff;

		if (!startingSpin) {	// if not the first spin, then allow the slots to be stopped after certain amount of time
			totalCosMovementCycles = 20;
			currentCosMovementCycle = 0;
			Invoke ("ReadyToStopTrue", 1f);
		} else {
			// starts the column spinning
			for (int i = 0; i < visibleAbilities.Length; i++) {
				visibleAbilities [i].GetComponent<AbilityScript> ().scrollSpeed = scrollSpeed;
			}
			
			// sets the speed of the abilities that might spawn to 0
			for (int i = 0; i < availableAbilitiesArray.Length; i++) {
				availableAbilitiesArray [i].GetComponent<AbilityScript> ().scrollSpeed = scrollSpeed;
			}
		}
	}

	private void ReadyToStopTrue () { // used with Invoke method
		slotsScript.readyToStop = true;
	}
}
