﻿using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float walkSpeed = 2;
	public float runSpeed = 3;
	public float deathBelowYPos = -10;
	public AnimationControl animationControl;
	
	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode jumpKey;
	public KeyCode runKey;
	
	private int frames;
	private Rigidbody2D rb;
	private float xScale;
	public int counter = 0;
	private bool isJumpPressed;
	private bool canJump;
	public bool jetpack;
	public int jetpackFrames;
	public int jetpackCounter;
	
	private Vector3 position;
	
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	
	///I like that joke, Joey - James

    ///Why thank you James - Joey

	
	void Start()
	{
		rb	     = GetComponent<Rigidbody2D>();
		xScale   =						   	 1;
		isJumpPressed  = 				 false;
		jetpackFrames  =                    30;
		counter        =                     0;
		jetpackCounter =                     0;
	}
	void Update()
	{
		//flips character depending on xScale
		transform.localScale = new Vector3(xScale, 1,1);
	}
	//Test for ground below player (to replenish jumps).
	void FixedUpdate()
	{
		//If player is below certain height, they die
		if(rb.position.y < deathBelowYPos)
		{
			PlayerStats playerStats = GetComponent<PlayerStats>();
			int health = playerStats.health; 
			
			//Sets health to 0
			playerStats.TakeDamage(health);
		}
		if(Input.GetKey(leftKey))
		{
			xScale = -1;
			if(Input.GetKey(runKey))
			{
				if(rb.velocity.x > -runSpeed)
				{
					rb.AddForce(Vector2.right * ((-runSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
				}
			}
			else
			{
				if(rb.velocity.x > -walkSpeed)
				{
					rb.AddForce(Vector2.right * ((-walkSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
				}
			}
		}
		if(Input.GetKey(rightKey))
		{
			xScale = 1;
			if(Input.GetKey(runKey))
			{
				if(rb.velocity.x < runSpeed)
				{
					rb.AddForce(Vector2.right * ((runSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
				}
			}
			else
			{
				if(rb.velocity.x < walkSpeed)
				{
					rb.AddForce(Vector2.right * ((walkSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
				}
			}
		}
		
		Vector3 offset;
		
		offset = new Vector3(0.15f, -0.01f, 0);
		RaycastHit2D groundHitRight = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.05f);
		
		offset = new Vector3(-0.15f, -0.01f, 0);
		RaycastHit2D groundHitLeft = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.05f);
		
		canJump = groundHitRight || groundHitLeft;
		
		animationControl.inAir = !canJump;
		
		
		
		frames = Mathf.Abs( (int)((20/walkSpeed) * rb.velocity.x) );
		
		if(Input.GetKey(jumpKey))
		{
			if(counter > 0)
			{
				counter++;
			}
			else
			{
				if(canJump)
				{
					counter++;
				}
			}
		}
		else
		{
			counter = 0;
		}
		//Condition for beginning to use jetpack
		if(Input.GetKey(jumpKey))
		{
			if(jetpackCounter > 0)
			{
				jetpackCounter++;
			}
			if(!canJump && !isJumpPressed)
			{
				jetpackCounter++;
			}
		}
		else
		{
			jetpackCounter = 0;
		}
		
		//Jumping controls
		if(Input.GetKey(jumpKey))
		{		
			if(counter <= frames && counter > 0)
			{
					rb.AddForce(Vector2.up * 4f, ForceMode2D.Force);
					//rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
			}
			if(!isJumpPressed)
			{
				if(canJump && counter == 1)
				{
					rb.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
					//rb.AddForce(Vector2.up * 1.8f, ForceMode2D.Impulse);
					canJump = false;
				}
			}
			//Jetpack
			if(jetpackCounter <= jetpackFrames && jetpackCounter > 0)
			{
					rb.AddForce(Vector2.up * 20f, ForceMode2D.Force);
					//rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
					if(!isJumpPressed){
						GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Jetpack");
					}
			}
		}
		else
		{
			GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("Jetpack");
		}
		
		isJumpPressed = Input.GetKey(jumpKey);
		
		Debug.DrawLine(new Vector3(rb.position.x,rb.position.y, 0), position, Color.green, 4, false);
		position = rb.position;
	}
	
}
