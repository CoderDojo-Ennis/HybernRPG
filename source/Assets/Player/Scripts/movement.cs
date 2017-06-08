using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float walkSpeed = 2;
	public float runSpeed = 3;
	public AnimationControl animationControl;
	
	public KeyCode leftKey;
	public KeyCode rightKey;
	public KeyCode jumpKey;
	public KeyCode runKey;
	
	private int frames;
	private Animator anim;
	private Rigidbody2D rb;
	private float xScale;
	private int counter = 0;
	private bool isJumpPressed;
	private bool canJump;
	
	private Vector3 position;
	
	//Why does a C programmer need glasses? Because he cant C#! hahahahaha
	
	///I like that joke, Joey - James

    ///Why thank you James - Joey

	
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
		transform.localScale = new Vector3(xScale, 1,1);
	}
	//Test for ground below player (to replenish jumps).
	void FixedUpdate()
	{		
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
		
		/*if(rb.velocity.y == 0)
		{
			animationControl.inAir = false;
		}
		if(rb.velocity.y <= -0.2)
		{
			animationControl.inAir = true;
		}
		if(rb.velocity.y >= 0.2)
		{
			animationControl.inAir = true;
		}*/
		
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
		}
		
		isJumpPressed = Input.GetKey(jumpKey);
		
		Debug.DrawLine(new Vector3(rb.position.x,rb.position.y, 0), position, Color.green, 4, false);
		position = rb.position;
	}
	
}
/*public void ForceMove(float force, Vector2 dir , Rigidbody2D rBody)
	{
		rBody.velocity = new Vector2 (0, 0);
		rBody.velocity += dir * force;
		Debug.Log(dir);
	}*/
	/*public void ForceMove(float force, Vector2 dir , Rigidbody2D rBody)
	{
		rBody.velocity = new Vector2 (0, 0);
		rBody.velocity += dir * force;
		Debug.Log(dir);
	}
	void Update () {
		
		//if (Input.GetKey (KeyCode.W)) {
		//	speed = 2;			
		//} else {
		//	speed = 1;
		//}
		

	}

	void FixedUpdate ()
	{
		RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position, Vector2.down * 0.05f);
		Rigidbody2D rb = GetComponent<Rigidbody2D> (); 
		
		if (Input.GetKey (leftKey)) { //move left
			xScale = 1f;
			if (Input.GetKey (runKey)) { 
				speed = 2;			
			} else { 
				speed = 1;
			}

			float h = Vector2.left.x;
			Vector3 v = rb.velocity;
			v.x = h * speed;
			rb.velocity = v; 

		} else if (Input.GetKey (rightKey)) { //move right
			xScale = -1f;
			if (Input.GetKey (runKey)) {
				speed = 2;			
			} else { 
				speed = 1;
			}
			float h = Vector2.right.x;
			Vector3 v = rb.velocity;
			v.x = h * speed;
			rb.velocity = v; 

		} else {
			speed = 0;
		}
		transform.localScale = new Vector3(xScale, 1f, 1f);
		anim.SetFloat ("speed", speed);
		if (Input.GetKeyDown (jumpKey)) { //jump
			//Debug.Log("jump!");
			if (jumps > 0) {
				anim.SetTrigger ("jump");
				//rb.velocity = new Vector2 (0, 0);
				//rb.velocity += new Vector2 (0, force + speed);
				ForceMove(force + speed, Vector2.up, rb);
				jumps--;
			}
		}
		if (Input.GetMouseButton(1)) { //aiming
			anim.SetBool ("aiming", true);
		} else {
			anim.SetBool ("aiming", false);
		}
		if (Input.GetMouseButton (0)) {
			anim.SetTrigger ("atk");
		}
		if(groundHit) {
			jumps = 2;
		}
	}*/
