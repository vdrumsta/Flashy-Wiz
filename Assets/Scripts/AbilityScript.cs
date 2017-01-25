using UnityEngine;
using System.Collections;

public class AbilityScript : MonoBehaviour {

	public bool spin;
	public float scrollSpeed;
	public float spawnChance = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (spin) {
			transform.Translate (0, -scrollSpeed * Time.deltaTime, 0);
		}
	}

	void OnBecameInvisible() { // only works properly if the editor window is closed
		Destroy (gameObject);
	}


}
