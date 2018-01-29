using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour {

	public float powerUpFireRate;
	public float floatSpeed;
	public float floatWait;
	public float respawnTime;
	AudioSource audioSource;
	SpriteRenderer sr;
	bool up = true;
	bool exists = true;
	// Use this for initialization
	void Start () {
		StartCoroutine(delay());
		audioSource = GetComponent<AudioSource>();
		sr = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if(up){
			transform.Translate(Vector3.up * Time.deltaTime * floatSpeed);
		}
		else{
			transform.Translate(Vector3.up * Time.deltaTime * floatSpeed * -1);
		}
	}


	IEnumerator delay(){
		yield return new WaitForSeconds (floatWait);
		up = !up;
		StartCoroutine(delay());
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(exists){
			// Debug.Log(other.gameObject.tag);
			if(other.gameObject.tag == "Player")
			{
				other.gameObject.GetComponent<PlayerInput>().getPowerUp(powerUpFireRate);
				audioSource.Play();
				sr.enabled = false;
				exists = false;
				StartCoroutine(waitSpawn());
			}
		}

	}


	IEnumerator waitSpawn(){
		yield return new WaitForSeconds (respawnTime);
		sr.enabled = true;
		exists = true;
	}
}
