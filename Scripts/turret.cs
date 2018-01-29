using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {

	public GameObject beamPrefab;
	public float fireDelay;
	bool fired = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (!fired) {
			var localOffset = new Vector2(2.0f,0);
			var worldOffset = transform.rotation * localOffset;
			var beamSpawn = transform.position + worldOffset;

			// Create the Bullet from the Bullet Prefab
			var beam = (GameObject)Instantiate (
			beamPrefab,
			beamSpawn,
			transform.rotation);
			// Add velocity to the bullet
			beam.GetComponent<Rigidbody2D>().velocity = beam.transform.right * 12;
			fired = true;
			StartCoroutine (Shoot ());
		}
	}

	IEnumerator Shoot(){
		yield return new WaitForSeconds (fireDelay);
		fired = false;
	}
}
