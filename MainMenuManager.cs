using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public class MainMenuManager : MonoBehaviour {


	public string level1;
	public string level2;
	AudioSource title;
	public GameObject background;
	public Sprite menuBG;
	public Sprite howToPlayBG;

	// UI elements to control
	public Button startButton;
	public Button startButton2;
	public Button menuButton;
	public Button controlsButton;
	public Text controlsText;

	void Awake(){
		title = GetComponent<AudioSource>();
		StartCoroutine (playTitle ());
		Time.timeScale = 1;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void GoToMainMenu() {
		//Change Background
		background.GetComponent<SpriteRenderer>().sprite = menuBG;
		startButton.gameObject.SetActive(true);
		startButton2.gameObject.SetActive(true);
		menuButton.gameObject.SetActive (false);
		controlsButton.gameObject.SetActive (true);
		controlsText.gameObject.SetActive (false);
	}

	public void showControls(){
		background.GetComponent<SpriteRenderer>().sprite = howToPlayBG;
		startButton.gameObject.SetActive(false);
		startButton2.gameObject.SetActive(false);
		controlsButton.gameObject.SetActive (false);
		menuButton.gameObject.SetActive (true);

		controlsText.gameObject.SetActive (true);
	}

	public void StartLevel1() {
		startButton.gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (loadLevel (level1));

	}

	public void StartLeve21() {
		startButton.gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (loadLevel (level2));

	}

	IEnumerator playTitle(){
		yield return new WaitForSeconds(3.4f);
		title.Play();
	}
	IEnumerator loadLevel(string level){
		yield return new WaitForSeconds(.6f);
		Application.LoadLevel(level);
	}
		
}
