using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomExplosion : MonoBehaviour 
{
	private PlayerStats playerStats;
	private CameraFollow cameraFollow;
	
	void Awake ()
	{
		//find camera script
		cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		
		//find playerStats script
		playerStats = GameObject.Find("Player Physics Parent").GetComponent< PlayerStats >();
		
		//Shake the screen
		StartCoroutine(cameraFollow.MyRoutine(1f, 0.3f, 0.3f));
		
		//Destroy the explosion object and turn off its collider after a period of time
		GameObject.Destroy ( gameObject, 4);
		
		this.Delay(0.2f, () => {
            GetComponent< Collider2D >().enabled = false;
        });
		
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		if( collider.gameObject.name == "Player Physics Parent" )
		{
			if (!playerStats.shielded)
			{
				playerStats.TakeDamage ( playerStats.health );
			}
			if (playerStats.shielded)
			{
				
			}
		}
		
		
		if( collider.gameObject.GetComponent<ShroomFire>() != null )
		{
			ShroomFire shroomFire;
			shroomFire = collider.gameObject.GetComponent< ShroomFire >();
			
			float delay = Random.Range(0.2f, 1);
			
			this.Delay( delay, () => {
				if (shroomFire != null)
				{
					shroomFire.BlowUp();
				}
            });
		}
		
		if( collider.gameObject.GetComponent<EnemyFramework>() != null )
		{
			EnemyFramework enemy;
			enemy = collider.gameObject.GetComponent< EnemyFramework >();
			
			enemy.TakeDamage ( enemy.health );
		}
	}
}
