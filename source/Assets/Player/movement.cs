using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float speed = 1;
	public int frames = 20;
	public int counter = 0;
	
	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode upKey;
	public KeyCode downKey;
	
	
	private Animator  anim;
	private Rigidbody2D rb;
	private float   xScale;
	private bool   isJumpPressed;
	public bool canJump;
	
	private Vector3 position;
	
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	
	///I like that joke, Joey - James
	
	void Start()
	{
		anim    =    GetComponent<Animator>();
		rb	    = GetComponent<Rigidbody2D>();
		xScale  = 							1;
		isJumpPressed =	 				false;
	}
	void Update()
	{
		
		
		//flips character depending on xScale
		transform.localScale = new Vector3(-xScale * 1.33974f, 1.33974f, 1.33974f);
	}
	//Test for ground below player (to replenish jumps).
	void FixedUpdate()
	{		
		RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position, Vector2.down * 0.05f);
		canJump = groundHit;
		
		if(Input.GetKey(leftKey))
		{
			xScale = -1;
			
			if(rb.velocity.x > -2)
			{
				rb.AddForce(Vector2.right * ((-2 - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
		if(Input.GetKey(rightKey))
		{
			xScale = 1;
			if(rb.velocity.x < 2)
			{
				rb.AddForce(Vector2.right * ((2 - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
		/*if(Input.GetKey(upKey))
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
		}*/
		/*
		//Jumping controls
		if(Input.GetKey(upKey))
		{
			
				
			if(counter <= frames && counter != 0)
			{
					rb.AddForce(Vector2.up * 0f, ForceMode2D.Force);
				//rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
			}
			if(!isJumpPressed)
			{
				
				if(canJump && counter == 1){
					rb.AddForce(Vector2.up * 1.8f, ForceMode2D.Impulse);
					//rb.AddForce(Vector2.up * 1.8f, ForceMode2D.Impulse);
					canJump = false;
				}
			}
		}
		
		isJumpPressed = Input.GetKey(upKey);
		
		Debug.DrawLine(new Vector3(rb.position.x,rb.position.y, 0), position, Color.green, 4, false);
		position = rb.position;*/
	}
	
}
