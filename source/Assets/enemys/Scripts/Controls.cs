using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode jumpKey;
	public KeyCode runKey;
	
	private EnemyFramework enemyFramework;
	
	void Start () 
	{
		enemyFramework = GetComponent<EnemyFramework>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(rightKey)){
			if(Input.GetKey(runKey)){
				enemyFramework.Run("right");
			}
			else{
				enemyFramework.Walk("right");
			}
		}
		if(Input.GetKey(leftKey)){
			if(Input.GetKey(runKey)){
				enemyFramework.Run("left");
			}
			else{
				enemyFramework.Walk("left");
			}
		}
		if(Input.GetKey(jumpKey)){
			enemyFramework.Jump();
		}
	}
}
