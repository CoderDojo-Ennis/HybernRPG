using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAttack : MonoBehaviour {

	void OnTriggerEnter2D ( Collider2D collider )
	{
		EnemyFramework enemy = collider.GetComponent<EnemyFramework>();
		if( enemy != null)
		{
			enemy.TakeDamage(1);
			return;
		}
		if(collider.gameObject.name == "überCultist")
		{
			collider.gameObject.GetComponent<UberCultistBehaviour>().TakeDamage(1);
		}
	}
}
