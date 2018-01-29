using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip fireClip;
	public AudioClip jumpClip;
	public AudioClip deathClip;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void fire(){
		audioSource.clip = fireClip;
		audioSource.Play();
	}
	public void jump(){
		audioSource.clip = jumpClip;
		audioSource.Play();
	}
	public void death(){
		audioSource.clip = deathClip;
		audioSource.Play();
	}
}
