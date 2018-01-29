using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour {
	Rigidbody2D rigidbody;
	Collider2D capsuleCollider;
	SpriteRenderer spriteRenderer;
	Animator animator;
	beamAudio beamAudio;
	public float speed = 12f;
	public Sprite iBeam;
	public GameObject mePrefab;
	public Material spriteMat;
	bool materialized = false;
	bool materializing = false;



	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		beamAudio = GetComponent<beamAudio>();
		//Physics.IgnoreLayerCollision (9, 9, true);
		//Physics.IgnoreLayerCollision (9, 8, true);
		// Add velocity to the bullet
		rigidbody.velocity = transform.right * speed;
	}

	// Update is called once per frame
	void Update () {
		if(!materialized){
			//Vector3 velocity = new Vector3(0f, 0f, 0.3f);
				//rigidbody.velocity.y * rigidbody.velocity.x
			Vector3 upVector = new Vector3(rigidbody.velocity.y, -rigidbody.velocity.x, 0f);
			Vector3 forwardVector = Vector3.Cross(rigidbody.velocity, upVector);

			transform.rotation = Quaternion.LookRotation(forwardVector, upVector);

			//Update velocity so beam does not slow down
			rigidbody.velocity = transform.right * speed;
		}
		if(gameObject.tag == "FallingBeam"){
			transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 8, Camera.main.transform);
			if(transform.position.y < -16.6){
				beamAudio.destroyBeam();
				var me = (GameObject)Instantiate (
				mePrefab,
				transform.position,
				transform.rotation);
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if((gameObject.tag == "Beam1" && other.tag == "Beam2") || (gameObject.tag == "Beam1" && other.tag == "BeamNeutral") || (gameObject.tag == "Beam2" && other.tag == "Beam1") || (gameObject.tag == "Beam2" && other.tag == "BeamNeutral")
		|| (gameObject.tag == "BeamNeutral" && other.tag == "Beam1") || (gameObject.tag == "BeamNeutral" && other.tag == "Beam2")){
			beamAudio.materialize();
			StartCoroutine(Materialize(other));
		}
		else if(other.tag == "Player" && materializing){
			other.gameObject.GetComponent<player>().death();
		}
		else
		{
			return;
		}
	}

	IEnumerator Materialize(Collider2D other)
  {
			materializing = true;
			gameObject.layer = 10;
      yield return new WaitUntil(() => other.gameObject.layer == 10);
			rigidbody.bodyType = RigidbodyType2D.Static;
			capsuleCollider.isTrigger = false;
			gameObject.tag = "MaterializedBeam";
			animator.enabled = false;
			spriteRenderer.sprite = iBeam;
			spriteRenderer.material = spriteMat;
			var me = (GameObject)Instantiate (
			mePrefab,
			transform.position,
			transform.rotation);

			materializing = false;
			materialized = true;
  }
}
