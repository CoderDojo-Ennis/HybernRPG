using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFramework : MonoBehaviour 
{

    //Combat variables
    public int attack;             //Flat damage dealt
	public int health;             //Maximum amount of damage an enemy can withstand
    public float maxSenseDistance;  //How far away the enemy can sense the player( square of distance)
	
	//Movement variables
	public float runSpeed;
	public float walkSpeed;
	public float jumpForce;
	private bool canJump;
	
	//Sounds for death
	public List<string> sounds;

    private Component[] rigidbodys;
    private Component[] bcolliders;
    private Component[] ccolliders;

    /**
	Beginning of enemy movement functions
	**/
    public void Walk(string direction)
	{
		//Finds Rigidbody2D of enemy
		Rigidbody2D rb;
		rb = GetComponent<Rigidbody2D>();
		
		//Checks string for direction and accelerates to
		//walkSpeed for each frame the function is called.
		if(direction == "right")
		{
			transform.localScale = new Vector3(1,1,1);
			if(rb.velocity.x < walkSpeed){
				rb.AddForce(Vector2.right * ((walkSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
		if(direction == "left")
		{
			transform.localScale = new Vector3(-1,1,1);
			if(rb.velocity.x > -walkSpeed){
					rb.AddForce(Vector2.right * ((-walkSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
	}
	public void Run(string direction)
	{
		//Finds Rigidbody2D of enemy
		Rigidbody2D rb;
		rb = GetComponent<Rigidbody2D>();
		
		//Checks string for direction and accelerates to
		//runSpeed for each frame the function is called.	
		if(direction == "right")
		{
			transform.localScale = new Vector3(1,1,1);
			if(rb.velocity.x < runSpeed){
				rb.AddForce(Vector2.right * ((runSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
		if(direction == "left")
		{
			transform.localScale = new Vector3(-1,1,1);
			if(rb.velocity.x > -runSpeed){
					rb.AddForce(Vector2.right * ((-runSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
	}
	public void Jump()
	{
		/**Currently, raycasts are used to detect if the enemy is on the ground.
		This is prone to create weird behaviour, as on some surfaces, both raycasts
		may not intersect with the terrain. (example: a bridge with holes in it)
		Caution also needs to be taken when setting up the gameObject and child sprites
		for the enemy so that the raycasts are indeed positioned at their feet.
		**/
		
		//Raycast on right
		Vector3 offset;
		offset = new Vector3(0.1f,0,0);
		RaycastHit2D groundHitRight = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.05f);
		
		//Raycast on left
		offset = new Vector3(-0.1f,0,0);
		RaycastHit2D groundHitLeft = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.05f);
		
		//Is enemy able to jump?
		canJump = groundHitRight || groundHitLeft;
		
		//If player is capable of jumping, an upward force is applied
		if(canJump)
		{
			//Finds Rigidbody2D of enemy
			Rigidbody2D rb;
			rb = GetComponent<Rigidbody2D>();
			
			//Applies jumpForce to player's Rigidbody2D
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}
    /**
	End of enemy movement functions
	**/

    /**
    Start of enemy combat functions
    **/
    //Waits

    public virtual void Attack()
	{

	}

	public virtual void TakeDamage(int damage)
	{
		health -= damage;
		if(health <= 0)
		{
            Die();
		}
	}

    public virtual void Die()
    {
		//Play death sound
		if(sounds.Count != 0)
		{
			int index = Random.Range(0, sounds.Count - 1);
			GameObject.Find("AudioManager").GetComponent<AudioManager>().Play(sounds[index]);
		}
		
		Component[] monos;
		
		GetComponent<EnemyFramework>().enabled = false;                     //Main Script disabled
        if (GetComponent<LineRenderer>())                                   //For LaserCultist, ...
            GetComponent<LineRenderer>().enabled = false;
        if (GetComponent<MonoBehaviour>())                                  //For MeleeCultist, ...
        {
            monos = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mono in monos)
            {
                mono.enabled = false;
            }
        }
		
		//Work with gameObject children first

        //Rigidbody2D
        rigidbodys = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rigidbodys)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(Random.Range(-3, 5), Random.Range(-3, 5));
        }

        //CircleColliders
        GetComponent<CapsuleCollider2D>().enabled = false;
        ccolliders = GetComponentsInChildren<CircleCollider2D>();
        foreach (CircleCollider2D cc in ccolliders)
        {
            cc.enabled = true;
        }

        //BoxColliders
        bcolliders = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D bc in bcolliders)
        {
            bc.enabled = true;
        }
		
		transform.GetChild(0).GetComponent<Animator>().enabled = false; //Cancel animations
		
		
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;   //Body flops
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<CapsuleCollider2D>().enabled = false;
        
		
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
