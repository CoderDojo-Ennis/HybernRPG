using UnityEngine;
using System.Collections;

public class enemyMove : MonoBehaviour {
	public GameObject[] targets;
	public float speed = 10;
	public float force = 2;
	public float range = 2;
	public float atkRange = 0.5f;
	public float xScale = 1;
	public int jumps = 2;
	public bool canjump = false;
	public Animator anim;
	public float jumpHeight = 0.5f;
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	void Start () {
		anim = GetComponent<Animator> ();
		transform.hasChanged = false;
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
		RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.left * xScale, 0.5f);
		RaycastHit2D jumpHit = Physics2D.Raycast(transform.position + new Vector3(0, jumpHeight,0), Vector2.left * xScale, 0.5f);
		//Debug.DrawRay(transform.position + new Vector3(0, jumpHeight,0), Vector2.left * 0.5f * xScale);
		//Debug.DrawRay(transform.position, Vector2.left * 0.5f * xScale);
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		targets = GameObject.FindGameObjectsWithTag("Good"); //stores all viable targets
		float highestWeight = 0;
		GameObject bestMatch = null;
		//Ray ray = new Ray(transform.position,Vector2.down);
		for (int i = 0; i < targets.Length; i++) { //goes through targets
			GameObject target = targets [i];
			if (Vector3.Distance (target.transform.position, transform.position) < range) { //if target within range
				float weight = Vector3.Distance (target.transform.position, transform.position);
				if (weight > highestWeight) { //chooses closest/most important target
					highestWeight = weight;
					bestMatch = target;
				}
			}
		}
		//Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude);
		if(bestMatch != null) { //if there is a best match
			if(Vector3.Distance (bestMatch.transform.position, transform.position) < atkRange) { //if target within atk range
				anim.SetTrigger("atk");
			} else { //if there is target but not in atk range
				speed = 1;
				float h = 0;
				Vector3 posBM = bestMatch.transform.position;
				Vector3 pos = transform.position;
				if(pos.x > posBM.x) { //move to target
					xScale = 1f;
					h = Vector2.left.x;
				} else {
					xScale = -1f;
					h = Vector2.right.x;
				}
				transform.localScale = new Vector3(xScale,1f,1f);
				Vector3 v = rb.velocity;
				v.x = h * speed;
				rb.velocity = v; 
				
				if(transform.hasChanged == false) {
					if(groundHit == true) {
						if(jumpHit == false) {
							rb.velocity = new Vector2 (0, 0);
							rb.velocity += new Vector2 (0, force + speed);
						}
					}
				} else {
					transform.hasChanged = false;
				}
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "floor") {
			jumps = 2;
		}
	}
}
