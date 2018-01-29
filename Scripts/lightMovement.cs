using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightMovement : MonoBehaviour {

	// Use this for initialization
	public float rotationSpeed;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// Debug.Log(transform.eulerAngles.y);
		if(transform.eulerAngles.y < 0 || transform.eulerAngles.y >= 280){
			transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
		}
	}
}
