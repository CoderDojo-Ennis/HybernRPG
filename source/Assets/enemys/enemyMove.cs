using UnityEngine;
using System.Collections;

public class enemyMove : MonoBehaviour {
	public GameObject[] targets;
	public GameObject lastTargetSeen = null;
	public GameObject bestMatch = null;
	public float speed = 10;
	public float jumpForce = 2;
	public float range = 2;
	public float atkRange = 0.5f;
	public float xScale = 1;
	public int jumps = 2;
	public bool canjump = false;
	public Animator anim;
	public float jumpHeight = 0.5f;
	public float timer = 0f;
	
	void Start () {
		anim = GetComponent<Animator> ();
		transform.hasChanged = false;
	}
	void Update () {
	}
	public void ForceMove(float force, Vector2 dir , Rigidbody2D rBody) {
		rBody.velocity = new Vector2 (0, 0);
		rBody.velocity += dir * force;
	}
	void FixedUpdate () {
		RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.left * xScale, 0.5f);
		RaycastHit2D jumpHit = Physics2D.Raycast(transform.position + new Vector3(0, jumpHeight,0), Vector2.left * xScale, 0.5f);
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		targets = GameObject.FindGameObjectsWithTag("Good"); //stores all viable targets
		float highestWeight = 0;
		bestMatch = null;
		for (int i = 0; i < targets.Length; i++) { //goes through targets
			GameObject target = targets [i];
				if(Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), target.transform.position - transform.position, range).collider.name == target.name) {
					float weight = Vector3.Distance (target.transform.position, transform.position);
					if (weight > highestWeight) { //chooses closest/most important target
						highestWeight = weight;
						bestMatch = target;
						lastTargetSeen = target;
					}
				}
			//}
		}
		GameObject t;
		if(bestMatch == null) {
			timer += Time.deltaTime;
			if(timer > 0.75f) {
				lastTargetSeen = null;
				timer = 0;
			}
			t = lastTargetSeen;
		} else {
			t = bestMatch;
			timer = 0;
		}
		if(t != null) {
			 //if there is a best match
			if(Vector3.Distance (t.transform.position, transform.position) < atkRange) { //if target within atk range
				anim.SetTrigger("atk");
				//bestMatch.GetComponent<movement>().ForceMove(2, new Vector2(-xScale, 0), bestMatch.GetComponent<Rigidbody2D> ());
			} else { //if there is target but not in atk range
				speed = 1;
				float h = 0;
				Vector3 posBM = t.transform.position;
				Vector3 pos = transform.position;
				if(pos.x > posBM.x) { //move to target
					xScale = 1f;
					h = Vector2.left.x;
				} else {
					xScale = -1f;
					h = Vector2.right.x;
				}
				transform.localScale = new Vector3(xScale, 1f, 1f);
				Vector3 v = rb.velocity;
				v.x = h * speed;
				rb.velocity = v; 
				
				if(transform.hasChanged == false) {
					if(groundHit == true) {
						if(jumpHit == false) {
							//rb.velocity = new Vector2 (0, 0);
							//rb.velocity += new Vector2 (0, jumpForce + speed);
							ForceMove(jumpForce + speed, Vector2.up, rb);
						}
					}
				} else {
					transform.hasChanged = false;
				}
			}
		}
		
	}
}
