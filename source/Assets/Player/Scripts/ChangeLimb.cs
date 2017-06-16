using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLimb : MonoBehaviour {

	private GameObject child;
	//Arm Limbs
	//0 - normal
	//1 - pickaxes
	//2 - shield
	//3-  grapplin hook
	//7 - arm cannon
	
	//Torso Limbs
	//0 - normal
	//1 - heavy torso
	
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
            animationControl.ArmLimbs = 0;
        }
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
            animationControl.ArmLimbs = 7;
        }
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
            animationControl.ArmLimbs = 3;
        }
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
            animationControl.ArmLimbs = 2;
        }
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
            //animationControl.ArmLimbs = 1;
        }
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
            animationControl.TorsoLimbs = 0;
        }
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
            animationControl.TorsoLimbs = 1;
        }
		if(Input.anyKey)
		{
			child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs);
		}
	}
}
