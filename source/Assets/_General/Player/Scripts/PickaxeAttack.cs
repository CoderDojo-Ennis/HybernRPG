﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAttack : MonoBehaviour {
	
	Pickaxes pickaxes;
	
	void OnEnable ()
	{
		pickaxes = transform.parent.gameObject.GetComponent<Pickaxes>();
	}
	
	void OnTriggerEnter2D ( Collider2D collider )
	{
		if( pickaxes.pickaxesEnabled && pickaxes.enabled )
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
}
