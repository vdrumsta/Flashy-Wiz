using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	public bool wall;
	public GameObject North;
	public GameObject East;
	public GameObject South;
	public GameObject West;
	public GameObject HitN;
	public GameObject HitE;
	public GameObject HitS;
	public GameObject HitW;
	public GameObject player;
	public Person personScript;

	private AudioSource audio;
	public AudioClip audioFire1;
	public AudioClip audioFire2;
	public AudioClip audioFire3;


	//Time delay
	public float attackTime = 1f;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	void Update (){
	}
		
	//			Movement
	public void moveUp (){
		if (!North.GetComponent<NextBlock> ().wall)
			transform.position += new Vector3 (0, 1, 0);
			//GameObject.Find ("Player").GetComponent<Player> ().turnTaken = !GameObject.Find ("Player").GetComponent<Player> ().turnTaken;
	}

	public void moveDown (){
		if (!South.GetComponent<NextBlock> ().wall)
			transform.position += new Vector3 (0, -1, 0);
			//GameObject.Find ("Player").GetComponent<Player> ().turnTaken = !GameObject.Find ("Player").GetComponent<Player> ().turnTaken;
	}

	public void moveLeft (){
		if (!West.GetComponent<NextBlock> ().wall)
			transform.position += new Vector3 (-1, 0, 0);
			//GameObject.Find ("Player").GetComponent<Player> ().turnTaken = !GameObject.Find ("Player").GetComponent<Player> ().turnTaken;
	}

	public void moveRight (){
		if (!East.GetComponent<NextBlock> ().wall)
			transform.position += new Vector3 (1, 0, 0);
			//GameObject.Find ("Player").GetComponent<Player> ().turnTaken = !GameObject.Find ("Player").GetComponent<Player> ().turnTaken;
	}

	//			Attacking
	public void attack(){
		HitN.SetActive (true);
		HitE.SetActive (true);
		HitS.SetActive (true);
		HitW.SetActive (true);

		AudioClip[] audioFireClips = new AudioClip[3];
		audioFireClips [0] = audioFire1;
		audioFireClips [1] = audioFire2;
		audioFireClips [2] = audioFire3;
		audio.PlayOneShot (audioFireClips [Random.Range (0, 3)], 0.5f);

		Invoke("stopAttack", attackTime);
	}

	void stopAttack()
	{
		HitN.SetActive (false);
		HitE.SetActive (false);
		HitS.SetActive (false);
		HitW.SetActive (false);
	}

}
