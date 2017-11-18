using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnorController : EnemyFramework 
{
	public override void Die()
	{
		GameObject.Find("WorldControl").GetComponent<WorldControl>().NextScene();
	}
}
