﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLimb : MonoBehaviour {
    AnimationControl animationControl;
    private GameObject child;

    public movement Movement;
    public GameObject wheel;
	public GameObject healthDisplay;
	public PlayerStats playerStats;
    
    /// is significantly incomplete
    
	//Arm Limbs
	//0 - normal
	///1 - pickaxes
	//2 - shield
	//3 - grappling hook
	//7 - arm cannon
	
	//Torso Limbs
	//0 - normal
	//1 - heavy torso
    //2 - jetpack
	//3 - cactus
	
	void Start ()
	{
		child = transform.GetChild(0).gameObject;
        animationControl = child.GetComponent<AnimationControl>();
        wheel = GameObject.Find("UI").transform.GetChild(0).gameObject;
		healthDisplay = GameObject.Find("UI").transform.GetChild(1).gameObject;
		playerStats = GetComponent< PlayerStats >();
    }

    void Awake()
    {
    }

    void Update ()
	{
		if(!playerStats.paused)
		{
			/*
			//Hotkeys disabled for time being
			//Arms
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SwitchArms(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				SwitchArms(7);
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				SwitchArms(3);
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				SwitchArms(2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				SwitchArms(1);
			}

			//Torso
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				SwitchTorso(3);
			}
			if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				SwitchTorso(2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha9))
			{
				SwitchTorso(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				SwitchTorso(1);
			}*/
			

			//Part Wheel
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				WheelControl();
			}
		}
    }

    public void SwitchArms(int i)
    {
        animationControl.ArmLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs, animationControl.HeadLimbs);
    }

    public void SwitchTorso(int i)
    {
        animationControl.TorsoLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, i, animationControl.HeadLimbs);
    }

    public void SwitchHead(int i)
    {
        animationControl.HeadLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs, animationControl.HeadLimbs);
    }

    public void WheelControl()
    {
        wheel.SetActive(!wheel.activeSelf);
		healthDisplay.SetActive(!healthDisplay.activeSelf);
        
        Movement.enabled = !Movement.enabled;
		
		
		//Time Control
		if( wheel.activeSelf)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
    }
}
