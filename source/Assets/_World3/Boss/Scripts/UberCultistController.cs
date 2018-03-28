using UnityEngine;

public class UberCultistController : MonoBehaviour {
	    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        //[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        public float m_VisionRange = 10f;
        public float m_AttackRange = 1f;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;             // Whether or not the player is grounded.
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
		
		public Transform jumpTarget;        //Target to jump to
		
		private AudioManager audioMan;      //audio manager

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform;
            m_Anim = transform.GetChild(0).GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
			audioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
		private void OnDisable()
		{
			//disable box colliders on arms
				Transform arms = transform.GetChild(0).GetChild(1);
				
				arms.GetComponent<BoxCollider2D>().enabled = false;
				arms.GetComponent<BoxCollider2D>().isTrigger = false;
		}

        private void FixedUpdate()
        {
            m_Grounded = false;
            
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
					
            }
			//m_Anim.SetBool("OnGround", m_Grounded);
			//m_Anim.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x/2));
        }
        
        public bool CheckVision(GameObject target)
        {
            RaycastHit2D ray = (Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), target.transform.position - transform.position, m_VisionRange));
            if (ray.collider == null)
            {
                //Debug.Log("target lost");
				//m_Anim.SetBool("Charging", false);
                return false;
            }
            else
            {
                if (ray.collider.name == target.name)
                {
                    Debug.DrawRay(transform.position + new Vector3(0, 0.6f, 0), target.transform.position - transform.position);
                    //Debug.Log("target found");
                    //m_Anim.SetBool("Charging", true);
					return true;
                }
                else
                {
                    //Debug.Log("target lost");
                    //m_Anim.SetBool("Charging", false);
					return false;
                }
            }
        }
        public void Move(float move, bool crouch, bool jump, bool attack)
        {

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character
                //Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                if (Mathf.Abs(m_Rigidbody2D.velocity.x) < m_MaxSpeed)
                {
					Vector2 direction = Vector2.right * move;
					float mass = m_Rigidbody2D.mass;
                    m_Rigidbody2D.AddForce(direction * mass * (m_MaxSpeed - m_Rigidbody2D.velocity.x)/5, ForceMode2D.Impulse);
                }
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump /*&& m_Anim.GetBool("Ground")*/)
            {
				//Variables for calculating jump velocity
				Vector2 origin;
				Vector2 target;
				float height;
				
				//Assign variables for calculating jump velocity
				origin = transform.position;
				target = jumpTarget.position;
				height = 2;
				
				//Calculate jump velocity
				m_Rigidbody2D.velocity = FindJumpVelocity(origin, target, height);
				
				//Character is off ground
				m_Grounded = false;
				//Tell the animator about this fact
				//m_Anim.SetBool("OnGround", m_Grounded);
            } 
            if(attack)
            {
                m_Anim.SetBool("Attack", true);
				
				//Sound of swinging axe
				//audioMan.Play("AxeSwing");

				//Enable box colliders on axe
				
				transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
				transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
            }
			else{
				 m_Anim.SetBool("Attack", false);
				//disable box colliders on axe
								
				transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
				transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
			}
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
		private Vector2 FindJumpVelocity(Vector2 origin, Vector2 target, float height)
	{
		//The goal here is to find the velocity needed to launch the rigidbody
		//from origin to target. Height is the distance above whichever of the
		//two points is heigher, the maximum height of the arc.
		//
		//initial speed = Sqrt(19.6 * maxDisplacement)
		//
		//
		Vector2 displacement;
		displacement = target - origin;
		
		float maxHeight;
		if(origin.y < target.y){
			//target is heigher point
			maxHeight = displacement.y + height;
			
		}else{
			//origin is heigher point
			maxHeight = height;
		}
		
		//Find y component of initial velocity
		Vector2 velocity;
		velocity.y = Mathf.Sqrt(19.62f * maxHeight);
		
		//Find y component of final velocity
		float finalVelocity;
		finalVelocity = -Mathf.Sqrt(19.62f * (maxHeight - displacement.y));
		
		//Find time taken
		float time;
		time = Mathf.Sqrt(Mathf.Abs((velocity.y - finalVelocity)/(9.81f)));
		
		//Find x component of initial velocity
		velocity.x = displacement.x/time;
		
		//Give back the answer
		return velocity;
	}
}
