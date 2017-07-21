using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
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
