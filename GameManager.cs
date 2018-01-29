using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public class GameManager : MonoBehaviour {

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	public static GameManager gm;

	// levels to move to on victory and lose
	public string resetLevel;
	public string menuLevel;
	public GameObject player1;
	public GameObject player2;
	// game performance

	// UI elements to control
	public Button resetButton;
	public Button menuButton;
	public Text player1Wins;
	public Text player2Wins;

	AudioSource audioSource;
	int _winner;
	public bool gameIsOver;
	// private variables

	Scene _scene;

	// set things up here
	void Awake () {
		Time.timeScale = 0f;
		audioSource = GetComponent<AudioSource> ();
		gameIsOver = false;
		// setup reference to game manager
		if (gm == null)
			gm = this.GetComponent<GameManager>();

		// setup all the variables, the UI, and provide errors if things not setup properly.
		setupDefaults();
		StartCoroutine (readyGo ());
		Debug.Log ("Starting Game");

	}

	// game loop
	void Update() {
		// if ESC pressed then pause the game
		if (player1 == null) {
			//Debug.Log ("Player 1 is dead");
			_winner = 2;
			if(!gameIsOver)
				gameOver ();
			gameIsOver = true;
		}
		if (player2 == null) {
			//Debug.Log ("Player 2 is dead");
			_winner = 1;
			if(!gameIsOver)
				gameOver ();
			gameIsOver = true;
		}
	}

	// setup all the variables, the UI, and provide errors if things not setup properly.
	void setupDefaults() {

		// get current scene
		_scene = SceneManager.GetActiveScene();


	}

	void gameOver(){
		
		//Debug.Log ("Game is over");
		Time.timeScale = 0f;
		resetButton.gameObject.SetActive(true);
		menuButton.gameObject.SetActive(true);
		if (_winner == 1) {
			player1Wins.gameObject.SetActive (true);
		} else {
			player2Wins.gameObject.SetActive (true);
		}
	}
		

	public void ResetLevel() {
		
		Application.LoadLevel(Application.loadedLevel);
	}
		
	public void GoToMainMenu() {
		SceneManager.LoadScene(menuLevel);
	}


	IEnumerator readyGo(){

		audioSource.Play ();
		yield return new WaitForSeconds(1f);
		Time.timeScale = 1f;

	}
}
