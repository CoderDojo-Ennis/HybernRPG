using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class enemyMove : EnemyFramework {
	public GameObject[] targets;
	public GameObject lastTargetSeen = null;
	public GameObject bestMatch = null;
	public float Speed = 10;
	public float jumpForce = 2;
	public float range = 2;
	public float atkRange = 0.5f;
	public float xScale = 1;
	//public int jumps = 2;
	public Animator anim;
	public float jumpHeight = 0.5f;
	public float chaseTimer = 0;
	public float idleTimer = 0;
	public float idleWalking = 0;
	public float idleTime = 2;
    private string enemyType;
    private Component EnemyType;
    private EnemyFramework newEnemy;
	void Start () {
		anim = GetComponent<Animator> ();
		transform.hasChanged = false;
        enemyType = gameObject.transform.name;
        Debug.Log(enemyType);
        EnemyType = gameObject.transform.GetComponent(enemyType);
        Debug.Log(EnemyType);
        newEnemy = new EnemyFramework();
        //newEnemy.EnemyClass = new ();       //Here is the error
        //newEnemy.Attack = ;
        //newEnemy.Beam = ;
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
		float highestWeight = 0;
		bestMatch = null;
		targets = GameObject.FindGameObjectsWithTag("Good"); //stores all viable targets
		for (int i = 0; i < targets.Length; i++) { //goes through targets
			GameObject target = targets [i];
			if(target != null) {
				RaycastHit2D checkVision = (Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), target.transform.position - transform.position, range));
				if(Vector3.Distance (target.transform.position, transform.position) < range) {
					if(checkVision.collider.name == target.name) {
						Debug.DrawRay(transform.position + new Vector3(0, 0.6f, 0), target.transform.position - transform.position);
						float weight = Vector3.Distance (target.transform.position, transform.position);
						if (weight > highestWeight) { //chooses closest/most important target
							highestWeight = weight;
							bestMatch = target;
							lastTargetSeen = target;
						}
					}
				}
			}
		}
		GameObject t = null;
		if(bestMatch == null) {
			chaseTimer += Time.deltaTime;
			if(chaseTimer > 1f) {
				lastTargetSeen = null;
				chaseTimer = 0;
			}
			t = lastTargetSeen;
		} else {
			t = bestMatch;
			chaseTimer = 0;
		}
		if(t != null) { //if there is a best match
			if(Vector3.Distance (t.transform.position, transform.position) < atkRange) { //if target within atk range
				/**Temporarily disabled. Error generated when trigger set - James**/
				//anim.SetTrigger("atk");
				Speed = 0;
				//bestMatch.GetComponent<movement>().ForceMove(2, new Vector2(-xScale, 0), bestMatch.GetComponent<Rigidbody2D> ());
			} else { //if there is target but not in atk range
				Speed = 1;
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
				v.x = h * Speed;
				rb.velocity = v; 
				
				if(transform.hasChanged == false) {
					if(groundHit == true) {
						if(jumpHit == false) {
							ForceMove(jumpForce + Speed, Vector2.up, rb);//jump
						}
					}
				} else {
					transform.hasChanged = false;
				}
			}
		} else { //if there is no best match
			idleTimer += Time.deltaTime;
			if(idleTimer > idleTime) {
				idleTimer = 0;
				idleWalking = Mathf.Round(UnityEngine.Random.Range(-1f, 1f));
				idleTime = UnityEngine.Random.Range(0.5f, 4f);
			}
			if(idleWalking != 0) {
				Speed = 1;
				xScale = -idleWalking;
				transform.localScale = new Vector3(xScale, 1f, 1f);
				Vector3 v = rb.velocity;
				v.x = idleWalking * Speed;
				rb.velocity = v; 
				if(transform.hasChanged == false) {
					if(groundHit == true) {
						if(jumpHit == false) {
							ForceMove(jumpForce + Speed, Vector2.up, rb); //jump
						}
					}
				} else {
						transform.hasChanged = false;
				}
			} else {
				Speed = 0;
			}
		}
	}
}