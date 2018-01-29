using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girderSpecial : MonoBehaviour {

	private GameObject girder;
	private Collider2D girderCollider;
	private GameObject player;
	private Collider2D playerCollider;
	private Transform t;
	private bool touching = false;

	void OnTriggerEnter2D(Collider2D other) {
        
		if (other.tag == "Player"){
			touching = true;
			player = other.gameObject;
		}
		
    }
	void OnTriggerExit2D(Collider2D other) {
        
		if (other.tag == "Player"){
			touching = false;
		}
		
    }

	void Start () {
		girderCollider = GetComponent<Collider2D> ();
		t = GetComponent<Transform>();
		
		//Debug.Log(Player);
		//Debug.Log(PlayerCollider);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(touching && t.position.y <= GameObject.FindWithTag("end").transform.position.y){
			t.Translate(Vector3.up * 1);
			player.transform.Translate(Vector3.up * 1);
		} else if ( !touching && t.position.y >= GameObject.FindWithTag("start").transform.position.y){
			t.Translate(Vector3.up * -1);
		}

		
	}
}
