using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	AudioManager AudioMan;

	public float walkSpeed = 2;
	public float runSpeed = 3;
	public float maxVelocity = 5;
	public bool jetpack;
	//[HideInInspector]
	public bool inWater;
	public float deathBelowYPos = -10;
	public AnimationControl animationControl;
	
	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode jumpKey;
	public KeyCode runKey;
	
	private int frames;
	private Rigidbody2D rb;
	private float xScale;
	private int counter = 0;
	private bool isJumpPressed;
	private bool canJump;
	private int jetpackFrames;
	public int jetpackCounter;
	private bool inWaterLastFrame;
	PlayerStats playerStats;
	
	private Vector3 position;
	
	//Particle system for jetpack
	private ParticleSystem jetpackFlames;
	
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	
	///I like that joke, Joey - James

    ///Why thank you James - Joey

	
	void Start()
	{
		AudioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		rb	     = GetComponent<Rigidbody2D>();
		xScale   =						   	 1;
		isJumpPressed  = 				 false;
		jetpackFrames  =                    30;
		counter        =                     0;
		jetpackCounter =                     0;
		inWaterLastFrame =               false;
		playerStats = GetComponent<PlayerStats>();
		jetpackFlames = GetComponentInChildren<ParticleSystem>();
		//Special code for Joey's level
		if( GameObject.Find("triangle") != null )
		{
			Physics2D.IgnoreCollision(GameObject.Find("triangle").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
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
			int health = playerStats.health; 
			
			//Sets health to 0
			playerStats.TakeDamage(health);
		}
		if(Input.GetKey(leftKey) && !playerStats.paused)
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
		if(Input.GetKey(rightKey) && !playerStats.paused)
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
		RaycastHit2D groundHitRight = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.1f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.1f);
		
		offset = new Vector3(-0.15f, -0.01f, 0);
		RaycastHit2D groundHitLeft = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.1f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.1f);
		
		canJump = groundHitRight || groundHitLeft;
		
		animationControl.inAir = !canJump;
		
		
		
		frames = Mathf.Abs( (int)((20/walkSpeed) * rb.velocity.x) );
		
		if(Input.GetKey(jumpKey) && !playerStats.paused)
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
		if(jetpack)
		{
			if(Input.GetKey(jumpKey) && !playerStats.paused)
			{
				if(jetpackCounter > 0){
					jetpackCounter++;
				}
				if(!canJump && !isJumpPressed && jetpackCounter == 0){
					jetpackCounter++;
				}
			}
			else
			{
				if(canJump || (inWater && !inWaterLastFrame)){
					jetpackCounter = 0;
				}
				else{
					if(jetpackCounter > jetpackFrames)
					{
						jetpackCounter = -1;
					}
				}
			}
		}
		else
		{
			jetpackCounter = 0;
			
		}
		
		//Jumping controls
		if(Input.GetKey(jumpKey) && !playerStats.paused)
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
			if(jetpack)
			{
				if(jetpackCounter <= jetpackFrames && jetpackCounter > 0)
				{
						rb.AddForce(Vector2.up * 20f, ForceMode2D.Force);
						//rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
						if(!isJumpPressed){
							AudioMan.Play("Jetpack");
							jetpackFlames.Play();
						}
						
				}
				if(jetpackCounter == jetpackFrames+1)
				{
					AudioMan.Stop("Jetpack");
					AudioMan.Play("Jetpack Stopped");
					
					jetpackFlames.Stop(false, ParticleSystemStopBehavior.StopEmitting);
				}
				
			}
			
		}
		else
		{
			AudioMan.Stop("Jetpack");
			jetpackFlames.Stop(false, ParticleSystemStopBehavior.StopEmitting);
		}
		
		isJumpPressed = Input.GetKey(jumpKey);
		inWaterLastFrame = inWater;
		
		Debug.DrawLine(new Vector3(rb.position.x,rb.position.y, 0), position, Color.green, 4, false);
		position = rb.position;
		
		//If player is going too fast, limit velocity
		/*if(rb.velocity.sqrMagnitude > maxVelocity * maxVelocity)
		{
			rb.velocity.Normalize();
			rb.velocity *= maxVelocity;
		}*/
	}
	
}
