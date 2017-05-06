﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFly : MonoBehaviour {

	public GrapplingHook grapplingHook;
	
	void OnEnable()
	{
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		
		//Hook not attatched to anything
		transform.parent = null;
		transform.localScale = new Vector3(1, 1, 1);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name != "Player Physics Parent"){
		grapplingHook.retract = true;
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
	
		//Attach hook to object it collides with
		transform.parent = collision.transform;
		transform.localScale = new Vector3(1/transform.parent.localScale.x,1/transform.parent.localScale.y,1/transform.parent.localScale.z);
		
		this.enabled = false;
		}
	}
}
