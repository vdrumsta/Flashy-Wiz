using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour {

	public GameObject heart1;
	public GameObject heart2;
	public GameObject heart3;
	public GameObject player;
	public GameObject DeathText;
	public GameObject WinText;
	public bool heart1B = true;
	public bool heart2B = true;
	public bool heart3B = true;
	public bool dead = false;
	public int health = 3;
	public Text keyCountText;
	public int keyCount = 0;
	public int deadTime = 2;

	private AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	void Update (){
		heart1.SetActive (heart1B);
		heart2.SetActive (heart2B);
		heart3.SetActive (heart3B);
		if (health == 3) {
			heart1B = true;
			heart2B = true;
			heart3B = true;
		}
		else if(health == 2){
			heart1B = false;
			heart2B = true;
			heart3B = true;
		}
		else if(health == 1){
			heart1B = false;
			heart2B = false;
			heart3B = true;
		}
		else{
			//Time.timeScale = 0.0f;
			heart1B = false;
			heart2B = false;
			heart3B = false;
			player.SetActive (false);
			dead = true;
			DeathText.SetActive (true);
			Invoke("death", deadTime);
		}
	}

	public int returnKeys(){
		return keyCount;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Damage" || col.gameObject.tag == "Hit"){
			health -= 1;
			audio.Play();
		}
		if(col.gameObject.tag == "Health"){
			if(health < 3)
				health += 1;
		}
		if(col.gameObject.tag == "Key"){
			keyCount++;
			keyCountText.text = "X " + keyCount;
		}
		if (col.gameObject.tag == "Door") {
			keyCount--;
			keyCountText.text = "Keys X " + keyCount;
		}
		if (col.gameObject.tag == "Portal") {
			WinText.SetActive (true);
			player.SetActive (false);
			Invoke("death", deadTime);
		}
	}

	void death (){
		SceneManager.LoadScene(0);
	}
}
