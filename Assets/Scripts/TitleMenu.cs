using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {

	void Start () {
		Time.timeScale = 1;
	}

	public void Level1(){

		SceneManager.LoadScene(1);
	}

	public void Level2(){

		SceneManager.LoadScene(2);
	}

	public void Quit(){

		Application.Quit ();
	}
}
