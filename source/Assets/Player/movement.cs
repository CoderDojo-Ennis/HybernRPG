using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public Vector3 v;
	//Rigidbody2D rb;
	public float speed = 10;
	public float force = 3;
	public int jumps = 2;
	public bool canjump = false;
	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode runKey;
	public KeyCode jumpKey;

	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	void Start () {
		v = new Vector3 (0, 0, 0);
	}

	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			speed = 2;			
		} else {
			speed = 1;
		}
		v.x = 0;
		if (Input.GetKey (leftKey)) {
			float h = Vector2.left.x;
			//v = GetComponent<Rigidbody2D> ().velocity;
			v.x = h * speed;
			//GetComponent<Rigidbody2D> ().velocity = v; 
		}

		if (Input.GetKey (rightKey)) {
			float h = Vector2.right.x;
			//v = GetComponent<Rigidbody2D> ().velocity;
			v.x = h * speed;
			//GetComponent<Rigidbody2D> ().velocity = v; 
		}
		//Debug.Log (v);
		//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
	}

	void FixedUpdate () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> (); 
		/*
		float h = Input.GetAxis ("Horizontal");
		Vector3 v = GetComponent<Rigidbody2D> ().velocity;
		v.x = h * speed;
		GetComponent<Rigidbody2D> ().velocity = v; 
		*/
		rb.velocity = v;
		/*
		if (Input.GetKey (leftKey)) {
			float h = Vector2.left.x;
			Vector3 v = GetComponent<Rigidbody2D> ().velocity;
			v.x = h * speed;
			GetComponent<Rigidbody2D> ().velocity = v; 
		}

		if (Input.GetKey (rightKey)) {
			float h = Vector2.right.x;
			Vector3 v = GetComponent<Rigidbody2D> ().velocity;
			v.x = h * speed;
			GetComponent<Rigidbody2D> ().velocity = v; 
		}
*/
		if (Input.GetKeyDown (jumpKey)) {
			Debug.Log("jump!");
			if (jumps > 0) {
				rb.velocity = new Vector2 (0, 0);
				rb.velocity += new Vector2 (0, force);
				jumps--;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "floor") {
			jumps = 2;
		}
	}
}
