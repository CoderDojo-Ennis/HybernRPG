using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour {

	//private PlayerStats playerStats;
	private CameraFollow cameraFollow;
	private AudioManager audioMan;
	
	
	void Awake ()
	{
		audioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		
		//find camera script
		cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		
		//find playerStats script
		//playerStats = GameObject.Find("Player Physics Parent").GetComponent< PlayerStats >();
		
		//Shake the screen
		StartCoroutine(cameraFollow.MyRoutine(1f, 0.3f, 0.3f));
		
		//Destroy the explosion object and turn off its collider after a period of time
		GameObject.Destroy ( gameObject, 4);
		
		audioMan.Play("Boom");
		
	}
}
