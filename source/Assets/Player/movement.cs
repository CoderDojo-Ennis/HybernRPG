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
		Rigidbody2D rb = GetComponent<Rigidbody2D> (); 
		if (Input.GetKey (leftKey)) {
			transform.localScale = new Vector3(1f,1f,1f);
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
			transform.localScale = new Vector3(-1f,1f,1f);
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
		anim.SetFloat ("speed", speed);
		if (Input.GetKeyDown (jumpKey)) { //jump
			Debug.Log("jump!");
			if (jumps > 0) {
				anim.SetTrigger ("jump");

				rb.velocity = new Vector2 (0, 0);
				rb.velocity += new Vector2 (0, force + speed);

				jumps--;
			}
		}
		if (Input.GetMouseButton(1)) {
			anim.SetBool ("aiming", true);
		} else {
			anim.SetBool ("aiming", false);
		}
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "floor") {
			jumps = 2;
		}
	}
}
