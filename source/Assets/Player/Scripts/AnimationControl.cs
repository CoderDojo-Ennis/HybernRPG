﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {
	
	public bool walking;
	public float speed;
	public bool inAir;
	public int ArmLimbs;
	void Update ()
	{
		
		speed = Mathf.Abs(GameObject.Find("Player Physics Parent").GetComponent<Rigidbody2D>().velocity.x)/2;
		if(speed < 0.05){
			walking = false;
		}
		else{
			walking = true;
		}
		
		this.transform.Find("head").GetComponent<Animator>().SetBool("Walking", walking);
		this.transform.Find("head").GetComponent<Animator>().SetFloat("Speed", speed);
		this.transform.Find("head").GetComponent<Animator>().SetBool("InAir", inAir);
		
		this.transform.Find("Arms").GetComponent<Animator>().SetBool("Walking", walking);
		this.transform.Find("Arms").GetComponent<Animator>().SetFloat("Speed", speed);
		this.transform.Find("Arms").GetComponent<Animator>().SetBool("InAir", inAir);
		this.transform.Find("Arms").GetComponent<Animator>().SetInteger("ArmLimbs", ArmLimbs);
		
		this.transform.Find("Legs").GetComponent<Animator>().SetBool("Walking", walking);
		this.transform.Find("Legs").GetComponent<Animator>().SetFloat("Speed", speed);
		this.transform.Find("Legs").GetComponent<Animator>().SetBool("InAir", inAir);
	}
	public void DisableAnimator()
	{
		//Turn of animator on child components
		//Head
		this.transform.Find("head").GetComponent<Animator>().enabled = false;
		//Arms
		this.transform.Find("Arms").GetComponent<Animator>().enabled = false;
		//Legs
		this.transform.Find("Legs").GetComponent<Animator>().enabled = false;
		
		//Disable scripts associated with limbs
		//Arms
		this.transform.Find("Arms").GetComponent<aiming>().enabled = false;
		this.transform.Find("Arms").GetComponent<GrapplingHook>().enabled = false;
		this.transform.Find("Arms").GetComponent<Pickaxes>().enabled = false;
	}
}
