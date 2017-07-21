using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusController : MonoBehaviour {

	void OnEnable()
	{
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<BoxCollider2D>().isTrigger = true;
	}
	void OnDisable()
	{
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<BoxCollider2D>().isTrigger = false;
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if( collider.gameObject.GetComponent<EnemyFramework>() != null )
		{
			collider.gameObject.GetComponent<EnemyFramework>().TakeDamage(1);
		}
	}
}
