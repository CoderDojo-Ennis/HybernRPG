using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		//Play a water splash sound
		if(collider.gameObject.GetComponent<Rigidbody2D>())
		{
			float fallSpeed;
			fallSpeed = collider.gameObject.GetComponent<Rigidbody2D>().velocity.y;
			
			if(fallSpeed < -4.4){
				GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Sizzle");
			}
			else{
				GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Sizzle");
			}
		}
		
		if(collider.gameObject.name == "Player Physics Parent")
		{
			PlayerStats playerStats = collider.gameObject.GetComponent<PlayerStats>();
			playerStats.TakeDamage( playerStats.health );
		}
		if( collider.gameObject.GetComponent<EnemyFramework>() )
		{
			EnemyFramework enemy = collider.gameObject.GetComponent<EnemyFramework>();
			enemy.TakeDamage( enemy.health);
		}
	}
}
