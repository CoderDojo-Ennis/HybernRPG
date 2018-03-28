using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
	
	private AudioManager audioMan;
	void OnEnable()
	{
		audioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if( collider.gameObject.name == "Player Physics Parent")
		{
			//Deduct health from player
			GameObject player;
			player = GameObject.Find("Player Physics Parent");
			
			player.GetComponent<PlayerStats>().TakeDamage(3);
			
			//Play sound of impact
			audioMan.Play("Impact");
		}
	}
}
