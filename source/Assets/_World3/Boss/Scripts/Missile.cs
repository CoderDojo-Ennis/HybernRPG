using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public GameObject explosion;
	
	void OnEnable()
	{
		Collider2D missile;
		Collider2D roof;
		
		missile = GetComponent<Collider2D>();
		roof = GameObject.Find("roof").GetComponent<Collider2D>();
		
		Physics2D.IgnoreCollision(roof, missile, true);
	}
	void OnCollisionEnter2D()
	{
		GameObject.Destroy( gameObject );
	}
	void OnDestroy()
	{
		GameObject.Instantiate( explosion, transform.position, Quaternion.identity);
	}
}
