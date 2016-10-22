using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float speed = 10;
	public float force = 3;
	public bool canjump = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		Vector3 v = GetComponent<Rigidbody2D> ().velocity;
		v.x = h * speed;
		GetComponent<Rigidbody2D> ().velocity = v; //this probably seems a little too complicated, but i dont know how to set the velocity of one axis without effecting the other axis any other way 
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (canjump == true) {
				GetComponent<Rigidbody2D> ().velocity += new Vector2 (0, force);
				canjump = false;

			}
		} 
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.name == "floor") {
			canjump = true;
		}
	}
}
