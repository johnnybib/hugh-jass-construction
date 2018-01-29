using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamAudio : MonoBehaviour {


	public AudioSource audioSource;
	public AudioClip materializeClip;
	public AudioClip destroyBeamClip;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void materialize(){
		audioSource.clip = materializeClip;
		audioSource.Play();
	}

	public void destroyBeam(){
		audioSource.clip = destroyBeamClip;
		audioSource.Play();
	}

}
