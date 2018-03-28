using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {
	
	public bool walking;
	public float speed;
	public bool inAir;
	
	public int ArmLimbs;
	public int TorsoLimbs;
    public int HeadLimbs;
	/// is significantly incomplete

	//Arm Limbs
	//0 - normal
	///1 - pickaxes
	//2 - shield
	//3 - grapplin hook
	///4 - wings
	//7 - arm cannon

	//Torso Limbs
	//0 - normal
	//1 - heavy torso
	///2 - cactus || jetpack
	private Animator Head;
	private Animator Arms;
	private Animator Torso;
	private Animator Legs;

	private void Start()
	{
		Head = transform.Find("head").GetComponent<Animator>();
		Arms = transform.Find("Arms").GetComponent<Animator>();
		Torso = transform.Find("torso").GetComponent<Animator>();
		Legs = transform.Find("Legs").GetComponent<Animator>();
	}

	void Update ()
	{
		Rigidbody2D rb;
		rb = GameObject.Find("Player Physics Parent").GetComponent<Rigidbody2D>();
		
		//See if player is walking
		speed = Mathf.Abs(rb.velocity.x);
		if(speed < 0.05){
			walking = false;
		}
		else{
			walking = true;
		}
		//See what kind of torso player has
		movement movement;
		movement = GameObject.Find("Player Physics Parent").GetComponent<movement>();
		CactusController cactusController;
		cactusController = GameObject.Find("Player Physics Parent").transform.GetChild(0).GetChild(2).GetComponent<CactusController>();
		
		if(TorsoLimbs == 0){
			//normal
			rb.mass = 1;
			movement.jetpack = false;
			cactusController.enabled = false;
		}

        if (TorsoLimbs == 1)
        {
            //heavy
            rb.mass = 3;
			movement.jetpack = false;
			cactusController.enabled = false;
        }

        if (TorsoLimbs == 2)
        {
            //normal
            rb.mass = 1f;
			movement.jetpack = true;
			cactusController.enabled = false;
        }
		if (TorsoLimbs == 3)
        {
            //normal
            rb.mass = 1f;
			movement.jetpack = false;
			cactusController.enabled = true;
        }

		//Scale speed down slightly
		speed /= 2;
		
        Head.SetBool("Walking", walking);
		Head.SetFloat("Speed", speed);
		Head.SetBool("InAir", inAir);
		
		Arms.SetBool("Walking", walking);
		Arms.SetFloat("Speed", speed);
		Arms.SetBool("InAir", inAir);
		Arms.SetInteger("ArmLimbs", ArmLimbs);
		
		Torso.SetInteger("TorsoLimbs", TorsoLimbs);
		
		Legs.SetBool("Walking", walking);
		Legs.SetFloat("Speed", speed);
		Legs.SetBool("InAir", inAir);
	}

	public void DisableAnimator()
	{
		//Turn of animator on child components
		//Head
		Head.enabled = false;
		//Arms
		Arms.enabled = false;
		//Legs
		Legs.enabled = false;
		
		//Disable scripts associated with limbs
		//Arms
		Arms.GetComponent<aiming>().enabled = false;
		Arms.GetComponent<GrapplingHook>().enabled = false;
		Arms.GetComponent<Pickaxes>().enabled = false;
	}
}
