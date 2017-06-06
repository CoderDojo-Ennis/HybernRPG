using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLimb : MonoBehaviour {

	private GameObject child;
	
	void Start ()
	{
		child = transform.GetChild(0).gameObject;
	}
	
	void Update ()
	{
		AnimationControl animationControl;
		animationControl = child.GetComponent<AnimationControl>();
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            //animationControl.ArmLimbs = 1;
        }
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
            animationControl.ArmLimbs = 7;
        }
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
            animationControl.ArmLimbs = 3;
        }
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
            animationControl.ArmLimbs = 0;
        }
	}
}
