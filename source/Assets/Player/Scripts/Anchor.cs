using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour {

	public GrapplingHook grapplingHook;
	
	public void Fire(Vector2 position, Vector2 velocity)
	{
		GetComponent<Rigidbody2D>().position = position;
		GetComponent<Rigidbody2D>().velocity = velocity;
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
	}
	void OnCollisionEnter2D()
	{
		grapplingHook.retract = true;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
	}
}
