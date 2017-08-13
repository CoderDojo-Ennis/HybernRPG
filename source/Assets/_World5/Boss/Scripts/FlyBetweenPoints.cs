using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBetweenPoints : MonoBehaviour {

	public Vector2 position1;
	public Vector2 position2;
	public float speed;

	private Rigidbody2D rb;
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.position = position1;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = VectorToTarget() * speed;
		rb.velocity += Mathf.Sin( (position2 - rb.position).magnitude );
	}
	Vector2 VectorToTarget ()
	{
		Vector2 vector = position2 - rb.position;
		vector.Normalize();
		return vector;
	}
}
