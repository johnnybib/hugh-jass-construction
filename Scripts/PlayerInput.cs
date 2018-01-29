using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	player player;
	public string horizontal, vertical, jump, fire, horizontalR, verticalR, altFire;
	public float altFireRate = 0.5f;
	float originalAltFireRate;
	bool fired = false;
	void Start () {
		player = GetComponent<player> ();
		originalAltFireRate = altFireRate;

	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw (horizontal), Input.GetAxisRaw (vertical));

		// Debug.Log (player.getArmRotation ());
		//Debug.Log(Input.GetAxisRaw("Horizontal2"));
		// player.setArmRotation(Mathf.Atan2 (Input.GetAxisRaw("Vertical2"), Input.GetAxisRaw("Horizontal2")) * 360 / 2 / Mathf.PI);
		if(Input.GetAxisRaw(horizontalR) == 0 && Input.GetAxisRaw(verticalR) == 0){
			player.setArmRotation(0);
		}
		else if (!player.GetComponent<playerController> ().getIsFacingRight ()) {
				player.setArmRotation(Mathf.Atan2 (Input.GetAxisRaw(verticalR), -Input.GetAxisRaw(horizontalR)) * 360 / 2 / Mathf.PI);
		}
		else{
			player.setArmRotation(Mathf.Atan2 (Input.GetAxisRaw(verticalR), Input.GetAxisRaw(horizontalR)) * 360 / 2 / Mathf.PI);
		}

		player.SetDirectionalInput (directionalInput);

		if(Input.GetButtonDown(jump) && player.getjumpCount() < 2){
			//player.doubleJump ();
			player.OnJumpInputDown ();
		}//else if (Input.GetButtonDown(jump)) {
			//player.OnJumpInputDown ();
		//}
		if (Input.GetButtonUp(jump)) {
			player.OnJumpInputUp ();
		}

		if (Input.GetButton (fire)) {
			if (!fired) {
				player.Fire (0);
				fired = true;
				StartCoroutine (Shoot ());
			}
		}
		if (Input.GetButton (altFire)) {
			if (!fired) {
				player.Fire (1);
				fired = true;
				StartCoroutine (ShootAlt ());
			}
		}

	}

	public void getPowerUp(float fireRate){
		altFireRate = fireRate;
		StartCoroutine(powerUpTimer ());
	}
	IEnumerator Shoot(){
		yield return new WaitForSeconds (0.1f);
		fired = false;
	}
	IEnumerator ShootAlt(){
		yield return new WaitForSeconds (altFireRate);
		fired = false;
	}
	IEnumerator powerUpTimer(){
		yield return new WaitForSeconds (5.0f);
		altFireRate = originalAltFireRate;
	}



}
