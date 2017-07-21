using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlast : MonoBehaviour {

	//GameObject wich spawned the object
	public GameObject creator;
	void Start ()
	{
		//GetComponent<Rigidbody2D>().AddForce(this.transform.rotation * new Vector3(0, -5, 0),ForceMode2D.Impulse);
		//Upon creation, ignore collisions between the projectile and the player
		if(creator != null)
		{
			Physics2D.IgnoreCollision(creator.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
		else{
			print("EnemyBlast instance needs to have a gameObject assigned to 'creator' variable, so it can ignore collisions with the enemy who spawned it");
		}
		GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Shoot Noise");
	}
	void Update()
	{
		//Point in direction of motion
		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle+90 , Vector3.forward);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.name == "Player Physics Parent")
		{
			collision.gameObject.GetComponent<PlayerStats>().TakeDamage(1);
		}
		if(collision.gameObject.GetComponent<EnemyFramework>() != null)
		{
			collision.gameObject.GetComponent<EnemyFramework>().TakeDamage(1);
		}
		if(collision.gameObject.name != "EnemyBlast(Clone)")
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}
