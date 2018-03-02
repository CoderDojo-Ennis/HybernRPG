using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFly : MonoBehaviour
{

	public GrapplingHook grapplingHook;

	void OnEnable()
	{
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

		//Hook not attatched to anything
		transform.parent = null;
		transform.localScale = new Vector3(1, 1, 1);

		//Enable collider
		GetComponent<BoxCollider2D>().enabled = true;

		//GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("HookFire");

	}
	void OnDisable()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		GetComponent<BoxCollider2D>().enabled = false;

		GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("HookChain");
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("HookChain");
		GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("HookFire");


		if (collision.gameObject.name != "Player Physics Parent" && collision.gameObject.tag != "Unhookable")
		{

			//Debug.Log(collision.gameObject.tag);
			grapplingHook.retract = true;

			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			//GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

			//Attach hook to object it collides with
			transform.parent = collision.transform;
			transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1 / transform.parent.localScale.z);

			//gameObject.GetComponent<SpringJoint2D>().connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();

			//Disable collider
			GetComponent<BoxCollider2D>().enabled = false;

			this.enabled = false;
		}
	}
}
