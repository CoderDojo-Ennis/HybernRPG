using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

	Vector3 startPosition;
	
	void Start()
	{
		startPosition = transform.position;
	}
	void Update()
	{
		transform.position = new Vector3( 0, 0.5f * Mathf.Sin(Time.time), 0 ) + startPosition;
	}
}
