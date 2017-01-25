using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	public float delay = 0.1f;
	public bool picked = false;
	private float now;
	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	void Update (){
		if(picked)
			if(Time.time >= now)
				gameObject.SetActive (false);
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			now = Time.time + delay;
			picked = true;
			audio.Play ();
		}
	}
}
