using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float speed = 10;
	public float force = 2;
	public int jumps = 2;
	public bool canjump = false;
	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode runKey;
	public KeyCode jumpKey;
	public Animator anim;
	public float xScale = 1f;
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		/*
		if (Input.GetKey (KeyCode.W)) {
			speed = 2;			
		} else {
			speed = 1;
		}
		*/

	}

	void FixedUpdate () {
		RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position, Vector2.down * 0.05f);
		Rigidbody2D rb = GetComponent<Rigidbody2D> (); 
		if (Input.GetKey (leftKey)) { //move left
			xScale = 1f;
			if (Input.GetKey (runKey)) { 
				speed = 2;			
			} else { 
				speed = 1;
			}

			float h = Vector2.left.x;
			Vector3 v = rb.velocity;
			v.x = h * speed;
			rb.velocity = v; 

		} else if (Input.GetKey (rightKey)) { //move right
			xScale = -1f;
			if (Input.GetKey (runKey)) {
				speed = 2;			
			} else { 
				speed = 1;
			}
			float h = Vector2.right.x;
			Vector3 v = rb.velocity;
			v.x = h * speed;
			rb.velocity = v; 

		} else {
			speed = 0;
		}
		transform.localScale = new Vector3(xScale, 1f, 1f);
		anim.SetFloat ("speed", speed);
		if (Input.GetKeyDown (jumpKey)) { //jump
			//Debug.Log("jump!");
			if (jumps > 0) {
				anim.SetTrigger ("jump");
				rb.velocity = new Vector2 (0, 0);
				rb.velocity += new Vector2 (0, force + speed);
				jumps--;
			}
		}
		if (Input.GetMouseButton(1)) { //aiming
			anim.SetBool ("aiming", true);
		} else {
			anim.SetBool ("aiming", false);
		}
		if (Input.GetMouseButton (0)) {
			anim.SetTrigger ("atk");
		}
		if(groundHit) {
			jumps = 2;
		}
	}
	void OnCollisionEnter2D(Collision2D col) { //if colliding with floor, reset jumps
		if(col.gameObject.name == "floor") {
			//jumps = 2;
		}
	}
}
