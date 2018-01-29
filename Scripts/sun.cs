using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour {

	public float sunSpeed;
	public float rotationSpeed;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(transform.position.y < 11)
		{
				transform.Translate(Vector3.up * Time.deltaTime * sunSpeed);
		}
		transform.GetChild(0).Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

	}
}
