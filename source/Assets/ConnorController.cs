using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnorController : EnemyFramework 
{
	public void Win()
	{
		LoadSceneAsync(16);
	}
}
