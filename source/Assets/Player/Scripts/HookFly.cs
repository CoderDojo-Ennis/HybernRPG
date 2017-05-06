using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFly : MonoBehaviour {

	public GrapplingHook grapplingHook;
	
	void OnEnable()
	{
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name != "Player Physics Parent"){
		grapplingHook.retract = true;
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		this.enabled = false;
		}
	}
}
