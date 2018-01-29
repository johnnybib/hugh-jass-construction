using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeCollision : MonoBehaviour {

	private GameObject spike;
	private Collider2D spikeCollider;
	private GameObject player;
	private Collider2D playerCollider;




	void Start () {
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player"){
			other.gameObject.GetComponent<player>().death();
		}

	}
}
