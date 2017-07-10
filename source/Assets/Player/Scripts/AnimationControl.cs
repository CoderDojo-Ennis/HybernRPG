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
		
        this.transform.Find("head").GetComponent<Animator>().SetBool("Walking", walking);
		this.transform.Find("head").GetComponent<Animator>().SetFloat("Speed", speed);
		this.transform.Find("head").GetComponent<Animator>().SetBool("InAir", inAir);
		
		this.transform.Find("Arms").GetComponent<Animator>().SetBool("Walking", walking);
		this.transform.Find("Arms").GetComponent<Animator>().SetFloat("Speed", speed);
		this.transform.Find("Arms").GetComponent<Animator>().SetBool("InAir", inAir);
		this.transform.Find("Arms").GetComponent<Animator>().SetInteger("ArmLimbs", ArmLimbs);
		
		this.transform.Find("torso").GetComponent<Animator>().SetInteger("TorsoLimbs", TorsoLimbs);
		
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
