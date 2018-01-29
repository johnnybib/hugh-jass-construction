using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestroy : MonoBehaviour {


	public GameObject dePrefab;
	public int lifeTime = 10;
    void Start(){
         // StartCoroutine(WaitThenDie());
    }
    // IEnumerator WaitThenDie(){
    //      yield return new WaitForSeconds(lifeTime);
    //      Destroy(gameObject);
    // }

	// Update is called once per frame
	void Update () {
		//If object hits bottom row of gourders
		if(transform.position.y < -16.6){
			var de = (GameObject)Instantiate (
			dePrefab,
			transform.position,
			transform.rotation);
			Destroy(gameObject);
		}
		transform.Translate(Vector3.up * -1/lifeTime);
	}
}
