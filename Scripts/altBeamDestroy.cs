using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altBeamDestroy : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == "MaterializedBeam"){
			Destroy(gameObject);
			other.tag = "FallingBeam";
		}

    }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
