using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCultist : EnemyFramework {
    //Sets variables from EnemyFramework
	void OnEnable()
	{
		walkSpeed = 7;
		runSpeed = 5;
		jumpForce = 4;
		health = 10;
	}
	override public void Attack()
    {
    }
}
