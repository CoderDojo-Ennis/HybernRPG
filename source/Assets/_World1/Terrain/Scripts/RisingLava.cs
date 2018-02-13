using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour {

	public float speed;
	
	void Start ()
	{
		Vector2 velocity;
		velocity = new Vector2(0, speed);
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
}
