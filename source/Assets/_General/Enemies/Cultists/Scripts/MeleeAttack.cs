using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		if( collider.gameObject.name == "Player Physics Parent")
		{
			//Deduct health from player
			GameObject player;
			player = GameObject.Find("Player Physics Parent");
			
			PlayerStats playerStats;
			player.GetComponent<PlayerStats>().TakeDamage(3);
		}
	}
}
