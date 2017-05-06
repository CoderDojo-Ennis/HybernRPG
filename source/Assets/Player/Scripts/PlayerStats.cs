using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int health;
	public CameraFollow cameraFollow;
	
	public void TakeDamage(int damage)
	{
		health -= damage;
		
		StopAllCoroutines();
		StartCoroutine(cameraFollow.MyRoutine(0.5f, 0.05f, 0.05f));
	}
}
