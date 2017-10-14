using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraDown : MonoBehaviour {

	public float desiredSpeed;
	public float lerpSpeed;
	
	private Rigidbody2D rb;
	void OnEnable () {
		rb = GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Kinematic;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 target;
		target = new Vector2(0, desiredSpeed);
		
		Vector2 result;
		result = Vector2.Lerp(rb.velocity, target, lerpSpeed);
		
		rb.velocity = result;
	}
}
