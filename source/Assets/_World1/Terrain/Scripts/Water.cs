using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		//Play a water splash sound
		if(collider.gameObject.GetComponent<Rigidbody2D>())
		{
			float fallSpeed;
			fallSpeed = collider.gameObject.GetComponent<Rigidbody2D>().velocity.y;
			
			if(fallSpeed < -4.4){
				GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Water Splash");
			}
			else{
				GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Water Splash 2");
			}
		}
		
		if(collider.gameObject.name == "Player Physics Parent")
		{
			collider.gameObject.GetComponent<movement>().inWater = true;
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.name == "Player Physics Parent")
		{
			collider.gameObject.GetComponent<movement>().inWater = false;
		}
	}
}
