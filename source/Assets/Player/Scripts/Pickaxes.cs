using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxes : MonoBehaviour {

	private Quaternion rotation1;
	private Quaternion rotation2;
	
	void Start ()
	{
		
	}
	
	void Update ()
	{
		GetComponent<Animator>().transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 90);
		if(Input.GetMouseButtonDown(0))
		{
				
		}
	}
}
