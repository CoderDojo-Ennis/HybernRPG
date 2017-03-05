using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlast : MonoBehaviour {

	void Start ()
	{
		GetComponent<Rigidbody2D>().AddForce(this.transform.rotation * new Vector3(0, -5, 0),ForceMode2D.Impulse);
	}
	void OnCollisionEnter2D()
	{
		GameObject.Destroy(this.gameObject);
	}
}
