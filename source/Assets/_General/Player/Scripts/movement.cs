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
	public GameObject ControllerManagerPrefab;
	public float jetpackSeconds;

	//public KeyCode leftKey;
	//public KeyCode rightKey;
	//public KeyCode jumpKey;
	//public KeyCode runKey;

	private float jetpackSecondsRemaining;
	//private int frames;
	private Rigidbody2D rb;
	private float xScale;
	//private int counter = 0;
	private bool isJumpPressed;
	private bool canJump;
	//private int jetpackFrames;
	//public int jetpackCounter;
	private bool inWaterLastFrame;
	private bool jumpTimer = true;
	private bool releasedButtonAfterJump = false;
	private bool jetpacking = false;
	PlayerStats playerStats;
	
	private Vector3 position;
	
	//Particle system for jetpack
	[HideInInspector]
	public ParticleSystem jetpackFlames;
	
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	
	///I like that joke, Joey - James

    ///Why thank you James - Joey

	
	void Start()
	{
		Instantiate(ControllerManagerPrefab);
		AudioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		rb	     = GetComponent<Rigidbody2D>();
		xScale   =						   	 1;
		isJumpPressed  = 				 false;
		//jetpackFrames  =                    30;
		//counter        =                     0;
		//jetpackCounter =                     0;
		inWaterLastFrame =               false;
		playerStats = GetComponent<PlayerStats>();
		jetpackFlames = GetComponentInChildren<ParticleSystem>();
		jetpackSecondsRemaining = jetpackSeconds;
		//Special code for Joey's level
		if (GameObject.Find("cultistDirector") != null)
		{
			Physics2D.IgnoreCollision(GameObject.Find("cultistDirector").GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}
	//Test for ground below player (to replenish jumps).
	void Update()
	{
		//If player is below certain height, they die
		if(rb.position.y < deathBelowYPos)
		{
			int health = playerStats.health; 
			
			//Sets health to 0
			playerStats.TakeDamage(health);
		}
		if (ControllerManager.instance.ControllerConnected == true)
		{
			if (Input.GetAxis("Horizontal") < 0 && !playerStats.paused)
			{
				xScale = -1;
				if (rb.velocity.x > -runSpeed)
				{
					rb.AddForce(Vector2.right * Mathf.Abs(Input.GetAxis("Horizontal")) * 10 * ((-runSpeed - rb.velocity.x) / 10), ForceMode2D.Impulse);
				}
			}
			else if (Input.GetAxis("Horizontal") > 0 && !playerStats.paused)
			{
				xScale = 1;
				if (rb.velocity.x < runSpeed)
				{
					rb.AddForce(Vector2.right * Mathf.Abs(Input.GetAxis("Horizontal")) * 10 * ((runSpeed - rb.velocity.x) / 10), ForceMode2D.Impulse);
				}
			}
		}
		else
		{
			if (Input.GetAxis("Horizontal") < 0 && !playerStats.paused)
			{
				xScale = -1;
				if (Input.GetButton("Run"))
				{
					if (rb.velocity.x > -runSpeed)
					{
						rb.AddForce(Vector2.right * ((-runSpeed - rb.velocity.x) / 10), ForceMode2D.Impulse);
					}
				}
				else
				{
					if (rb.velocity.x > -walkSpeed)
					{
						rb.AddForce(Vector2.right * ((-walkSpeed - rb.velocity.x) / 10), ForceMode2D.Impulse);
					}
				}
			}
			else if (Input.GetAxis("Horizontal") > 0 && !playerStats.paused)
			{
				xScale = 1;
				if (Input.GetButton("Run"))
				{
					if (rb.velocity.x < runSpeed)
					{
						rb.AddForce(Vector2.right * ((runSpeed - rb.velocity.x) / 10), ForceMode2D.Impulse);
					}
				}
				else
				{
					if (rb.velocity.x < walkSpeed)
					{
						rb.AddForce(Vector2.right * ((walkSpeed - rb.velocity.x) / 10), ForceMode2D.Impulse);
					}
				}
			}
		}
		
		Vector3 offset;
		
		offset = new Vector3(0.12f, 0.08f, 0);
		RaycastHit2D groundHitRight = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.2f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.2f);
		
		offset = new Vector3(-0.12f, 0.08f, 0);
		RaycastHit2D groundHitLeft = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.2f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.2f);
		
		canJump = groundHitRight || groundHitLeft;
		
		animationControl.inAir = !canJump;
		
		//Start of jumping + jetpack recode
		if (canJump)
		{
			jetpackSecondsRemaining = jetpackSeconds;
		}
		if ((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0) && !playerStats.paused)
		{
			if (jumpTimer && canJump)
			{
				releasedButtonAfterJump = false;
				rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
				jumpTimer = false;
				StartCoroutine("JumpTimer");
			}
			if (jetpack && jetpackSecondsRemaining > 0 && !canJump && releasedButtonAfterJump)
			{
				if (jetpacking == false)
				{
					AudioMan.Play("Jetpack");
					jetpackFlames.Play();
					jetpacking = true;
				}
				//rb.AddForce(Vector2.up * 20f, ForceMode2D.Force);
				jetpackSecondsRemaining -= Time.deltaTime;
			}
		}
		if (jetpacking && !(Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0))
		{
			jetpacking = false;
			AudioMan.Stop("Jetpack");
			jetpackFlames.Stop(false, ParticleSystemStopBehavior.StopEmitting);
		}
		if (jetpackSecondsRemaining <= 0 && jetpackSecondsRemaining > -10f)
		{
			jetpacking = false;
			AudioMan.Stop("Jetpack");
			AudioMan.Play("Jetpack Stopped");
			jetpackFlames.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			jetpackSecondsRemaining = -100f;
		}
		if (ControllerManager.instance.ControllerConnected)
		{
			if (Input.GetButtonUp("Jump") || !(Input.GetAxis("Vertical") > 0))
			{
				releasedButtonAfterJump = true;
			}
		}
		else
		{
			if (Input.GetButtonUp("Jump"))
			{
				releasedButtonAfterJump = true;
			}
		}
		/*
		frames = Mathf.Abs( (int)((20/walkSpeed) * rb.velocity.x) );
		
		if((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0) && !playerStats.paused)
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
			if((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0) && !playerStats.paused)
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
		if((Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0) && !playerStats.paused)
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
		
		isJumpPressed = Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0;
		*/
		inWaterLastFrame = inWater;
		
		Debug.DrawLine(new Vector3(rb.position.x,rb.position.y, 0), position, Color.green, 4, false);
		position = rb.position;

		//If player is going too fast, limit velocity
		/*if(rb.velocity.sqrMagnitude > maxVelocity * maxVelocity)
		{
			rb.velocity.Normalize();
			rb.velocity *= maxVelocity;
		}*/
		//Flip character
		transform.localScale = new Vector3(xScale, 1, 1);
	}

	private void FixedUpdate()
	{
		if (jetpacking)
		{
			rb.AddForce(Vector2.up * 20f, ForceMode2D.Force);
		}
	}

	IEnumerator JumpTimer()
	{
		yield return new WaitForSeconds(0.1f);
		jumpTimer = true;
	}
	
}
