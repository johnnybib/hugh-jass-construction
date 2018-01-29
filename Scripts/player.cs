using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (playerController))]

public class player : MonoBehaviour {

	public GameObject beamPrefab;
	public GameObject altBeamPrefab;

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	public float timeScale = 0.5f;

	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
	float armRotation;


	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	bool isDead;

	playerController controller;
	playerAudio playerAudio;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;


	private bool inAir = false;
	private Transform thisObjTrans;
	private int jumpCount = 0;
	public Animator anim;

	void Start() {
		thisObjTrans = GetComponent<Transform> ();
		controller = GetComponent<playerController> ();
		playerAudio = GetComponent<playerAudio>();
		anim = GetComponent<Animator> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		Time.timeScale = timeScale;;
		isDead = false;
	}

	void Update() {
		if (!isDead) {
			CalculateVelocity ();
			HandleWallSliding ();

			controller.Move (velocity * Time.deltaTime, directionalInput);
			transform.GetChild(0).eulerAngles = new Vector3 (0, transform.GetChild(0).eulerAngles.y, armRotation);
			if (controller.collisions.above || controller.collisions.below) {
				if (controller.collisions.slidingDownMaxSlope) {
					velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
				} else {
					velocity.y = 0;
				}
			}

			if(controller.collisions.below){
				inAir = false;
				jumpCount = 0;
				//Debug.Log(jumpCount);
			}

			//Animations

			if (Mathf.Abs(velocity.x) > 0.01 ) {
				anim.SetBool ("isWalking", true);
			} else {
				anim.SetBool ("isWalking", false);
			}

			//Debug.Log(inAir);
		}

	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		playerAudio.jump();
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if ((controller.collisions.below && jumpCount == 0) || jumpCount < 2) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			}else{
				velocity.y = maxJumpVelocity;
				inAir = true;
				jumpCount += 1;
				//Debug.Log("dog" + jumpCount);
			}

		}

	}

	// public void doubleJump(){
	// 	velocity.y *= 5;
	// 	inAir = false;
	// 	//Debug.Log("dj");
	// }

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
			//jumpCount += 1;
			//Debug.Log("cat" + jumpCount);
		}
	}

	public void Fire(int select){
		playerAudio.fire();
		var localOffset = new Vector2(4.0f,0);
		var worldOffset = transform.GetChild(0).rotation * localOffset;
		var beamSpawn = transform.GetChild(0).position + worldOffset;

		// Create the Bullet from the Bullet Prefab
		if(select == 0){
			var beam = (GameObject)Instantiate (
			beamPrefab,
			beamSpawn,
			transform.GetChild(0).rotation);
			// Add velocity to the bullet
			beam.GetComponent<Rigidbody2D>().velocity = beam.transform.right * 12;
		} else if (select == 1){
			var beam = (GameObject)Instantiate (
			altBeamPrefab,
			beamSpawn,
			transform.GetChild(0).rotation);
			// Add velocity to the bullet
			beam.GetComponent<Rigidbody2D>().velocity = beam.transform.right * 12;
		} else{

		}
	}


	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;
			//Wall jumping
			//jumpCount = 1;
			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

	public void death(){
		playerAudio.death();
		isDead = true;
		velocity = new Vector3(0,0,0);
		anim.SetTrigger ("Dead");

		// Debug.Log("DEAD");
		StartCoroutine (deathDestroy ());
	}
	IEnumerator deathDestroy(){
		yield return new WaitForSeconds (1.0f);
		Destroy(gameObject);
	}


	public void setArmRotation(float newArmRotation){
		armRotation = newArmRotation;
	}

	public float getArmRotation(){
		return armRotation;
	}

	public bool getinAir(){
		return inAir;
	}
	public int getjumpCount(){
		return jumpCount;
	}
}
