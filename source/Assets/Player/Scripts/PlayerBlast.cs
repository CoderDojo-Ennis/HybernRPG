using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlast : MonoBehaviour {

	void Start ()
	{
		GetComponent<Rigidbody2D>().AddForce(this.transform.rotation * new Vector3(0, -5, 0),ForceMode2D.Impulse);
		//Upon creation, ignore collisions between the projectile and the player
		Physics2D.IgnoreCollision(GameObject.Find("Player Physics Parent").GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	void Update()
	{
		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis (angle+90 , Vector3.forward);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.GetComponent<EnemyFramework>().TakeDamage(1);
			GameObject.Destroy(this.gameObject);
		}
		if(collision.gameObject.name != "Player Physics Parent" && collision.gameObject.name != "PlayerBlast(Clone)")
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}
