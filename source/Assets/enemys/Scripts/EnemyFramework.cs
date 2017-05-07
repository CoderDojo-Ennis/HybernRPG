using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFramework : MonoBehaviour {

    public GameObject LaserBeam;

    //Stats in alphabetical order
    //Most unimplemented
    private int attack;             //Flat damage dealt
    private float attackRangeMax;   //Minimum distance to attack (ranged)
    private float attackRangeMin;   //Maximum distance to attack (ranged)
    private bool beam;              //Can use BeamAttack()
    private int health;             //Maximum amount of damage an enemy can withstand
    private int jumps;              //How many times can they jump
    private float jumpHeight;       //How high can they jump
    private bool melee;             //Can use MeleeAttack()
    private bool projectile;        //Can use ProjectileAttack()
    private float speed;            //How fast they can move

    public int Attack { get; set; }
    public float AttackRangeMax { get; set; }
    public float AttackRangeMin { get; set; }
    public bool Beam { get; set; }
    public int Health { get; set; }
    public int Jumps { get; set; }
    public float JumpHeight { get; set; }
    public bool Melee { get; set; }
    public bool Projectile { get; set; }
    public float Speed { get; set; }
	/**
	Beginning of properties Added by James
	**/
	public float runSpeed;
	public float walkSpeed;
	public float jumpForce;
	public bool canJump;
	/**End of properties Added by James
	**/

	/**
	Beginning of functions added by James
	**/
	public void Walk(string direction)
	{
		Rigidbody2D rb;
		rb = GetComponent<Rigidbody2D>();
		
		if(direction == "right")
		{
			if(rb.velocity.x < walkSpeed){
				rb.AddForce(Vector2.right * ((walkSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
		if(direction == "left")
		{
			if(rb.velocity.x > -walkSpeed){
					rb.AddForce(Vector2.right * ((-walkSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
	}
	public void Run(string direction)
	{
		Rigidbody2D rb;
		rb = GetComponent<Rigidbody2D>();
		
		if(direction == "right")
		{
			if(rb.velocity.x < runSpeed){
				rb.AddForce(Vector2.right * ((runSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
		if(direction == "left")
		{
			if(rb.velocity.x > -runSpeed){
					rb.AddForce(Vector2.right * ((-runSpeed - rb.velocity.x)/10), ForceMode2D.Impulse);
			}
		}
	}
	public void Jump()
	{
		//Use raycasts to check if enemy is on ground.
		//Colliders and sprites must be positioned so that
		//raycasts can touch ground when fired.
		Vector3 offset;
		offset = new Vector3(0.1f,0,0);
		RaycastHit2D groundHitRight = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.05f);
		
		offset = new Vector3(-0.1f,0,0);
		RaycastHit2D groundHitLeft = Physics2D.Raycast(transform.position + offset, Vector2.down, 0.05f);
		Debug.DrawRay(transform.position + offset, Vector2.down * 0.05f);
		canJump = groundHitRight || groundHitLeft;
		
		//If player is capable of jumping, an upward force is applied
		if(canJump)
		{
			canJump = true;
		}
		if(canJump)
		{
			Rigidbody2D rb;
			rb = GetComponent<Rigidbody2D>();
			
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			//As player is jumping, they should no longer be able to jump
			canJump = false;
		}
	}
	/**
	End of functions added by James
	**/
    //Default attack in close quarters
    public void MeleeAttack()
    {

    }
    
    //Default ranged attack affected by gravity
    public void ProjectileAttack()
    {

    }

    //Default ranged attack in straight line
    public void BeamAttack(GameObject target)
    {
        Component Control;
        Control = LaserBeam.GetComponent("LaserControl");
        //LaserBeam.getComponent("LaserControl").LaserBehaviour();
        Vector3 lastPos;
        Vector3 targetPos;
        float lerpMove = 0;
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0f, 0.6f, 0f), target.transform.position - transform.position, Color.red);
        Ray2D ray = new Ray2D(transform.position + new Vector3(0f, 0.6f, 0f), target.transform.position - transform.position);
        hit = new RaycastHit();
        lastPos = transform.position;
        targetPos = hit.point;
        lerpMove += Time.deltaTime;
        Instantiate(LaserBeam);
        //LaserBeam.transform.position = Vector3.MoveTowards(transform.position + new Vector3(0f, 0.6f, 0f), target.transform.position, 2 * Time.deltaTime);
        LaserBeam.transform.position = Vector3.MoveTowards(lastPos, targetPos, 10 * lerpMove);
        Debug.Log("Over");
        
    }
    //Only called in enemyBehaviour
    //'s' is equal to enemyType
    public EnemyFramework EnemyCreation(string s)
    {
        switch (s)
        {
            //Sorted alphabetically
            case "ArmCannonCultist":
                return new ArmCannonCultist();
            case "LaserCultist":
                return new LaserCultist();
            case "MeleeCultist":
                return new MeleeCultist();
            default:
                return null;
        }
    }
}
