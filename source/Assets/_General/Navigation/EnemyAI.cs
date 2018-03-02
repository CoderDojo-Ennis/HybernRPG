using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class EnemyAI : MonoBehaviour {
    private PlatformerCharacter2D Character;
    public GameObject NavPointContainer;
    //public GameObject PlayerGO;
    public GameObject Player;
    public Thoughts Thought;
    public NavPoint LastNavPoint;
    public NavPoint TargetNavPoint;

    private NavPoint[] AllNavPoints;
	private bool agro;

    public enum Thoughts
    {
        Idle,
        WalkLeft,
        WalkRight,
        JumpLeft,
        JumpRight,
        Attack
    }

    private void Awake()
    {
		Player = GameObject.Find("Player Physics Parent");
		NavPointContainer = GameObject.Find("NavPoints");
		
        Thought = Thoughts.Idle;
        Character = GetComponent<PlatformerCharacter2D>();
        AllNavPoints = NavPointContainer.GetComponentsInChildren<NavPoint>();
        LastNavPoint = NavPoint.FindClosestNavPoint(this.transform.position, AllNavPoints);
        //Debug.Log(this.name + " starting near " + LastNavPoint.name);
		agro = false;
		
    }

    private void Start()
    {	
        Think();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = false;
        bool jump = false;
        bool attack = false;
        float xVelocity = 0;
        float x = 1f;

        switch (this.Thought)
        {
            case Thoughts.Idle:
                xVelocity = 0;
                break;
            case Thoughts.WalkLeft:
                xVelocity = -x;
                break;
            case Thoughts.WalkRight:
                xVelocity = x;
                break;
            case Thoughts.JumpLeft:
                xVelocity = -x;
                jump = true;
                break;
            case Thoughts.JumpRight:
                xVelocity = x;
                jump = true;
                break;
            case Thoughts.Attack:
                xVelocity = 0;
                attack = true;
                break;
        }
        //Debug.Log(transform.hasChanged);
        // Pass all parameters to the character control script.
        Character.Move(xVelocity, crouch, jump, attack);

        // Todo - think after landing/*
        if (Thought == Thoughts.JumpLeft)
        {
            //Thought = Thoughts.WalkLeft;
            Think();
        }
        else if (Thought == Thoughts.JumpRight)
        {
            //Thought = Thoughts.WalkRight;
            Think();
        }

        // Check if player is in attack range
        float dist = (transform.position - Player.transform.position).sqrMagnitude;
        if (dist <= Character.m_AttackRange * Character.m_AttackRange)
        {
            Thought = Thoughts.Attack;
        }
        if (Thought == Thoughts.Attack && dist > Character.m_AttackRange * Character.m_AttackRange)
        {
            this.Delay(1, Think);
        }
		///Experimental agro system
		if(!agro)
		{
			GameObject[] targets = GameObject.FindGameObjectsWithTag("Good");
			agro = Character.CheckVision(targets[0]);
		}
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var navPoint = collision.gameObject.GetComponent<NavPoint>();
        if (navPoint != null)
        {
            if (navPoint != LastNavPoint)
            {
                //Debug.Log("Hit nav point " + navPoint.gameObject.name);
                LastNavPoint = navPoint;
                Think();
            }
        }
    }

    private void Think()
    {
        // Check if player can be seen
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Good");
        float closestDist = Character.m_VisionRange * Character.m_VisionRange;
        GameObject bestMatch = null;
        for (int i = 0; i < targets.Length; i++)
        {
            /**if (Character.CheckVision(targets[i]))
            {
                float dist = (transform.position - targets[i].transform.position).sqrMagnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    bestMatch = targets[i]; //best match is the closest target that can be seen
                }
            }*/
			///Trying out permanent agro system
			if (agro)
            {
                float dist = (transform.position - targets[i].transform.position).sqrMagnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    bestMatch = targets[i]; //best match is the closest target that can be seen
                }
            }
        }

        Thought = Thoughts.Idle;
        if (bestMatch != null)
        {
            Player = bestMatch;
            NavPointPath path = FindPathToTarget(Player.transform.position);


            if (path != null && path.Neighbors != null && path.Neighbors.Count > 0)
            {
                // Whats the next action
                var neighbor = path.Neighbors[0];
				
				//Find transform of next navpoint, just in case we want to jump to it
				Character.jumpTarget = neighbor.NeighborPoint.transform;
				
				
                var neighborVector = neighbor.NeighborPoint.transform.position - this.transform.position;
                switch (neighbor.TravelType)
                {
                    case TravelTypes.Walk:
                        if (neighborVector.x > 0)
                        {
                            Thought = Thoughts.WalkRight;
                        }
                        else
                        {
                            Thought = Thoughts.WalkLeft;
                        }
                        break;
                    case TravelTypes.Jump:
                        if (neighborVector.x > 0)
                        {
                            Thought = Thoughts.JumpRight;
                        }
                        else
                        {
                            Thought = Thoughts.JumpLeft;
                        }
                        break;
                }
            }
        }
        /*
        if (transform.hasChanged == false)
        {
            if (Thought != Thoughts.Idle && Thought != Thoughts.Attack)
            {
                Think();
            }
        }
        else
        {
            transform.hasChanged = false;
        }*/
        if (Thought == Thoughts.Idle)
        {
            this.Delay(1, Think);
        }


        //Debug.Log("Thought = " + Thought);
    }

    private NavPointPath FindPathToTarget(Vector3 target)
    {
        TargetNavPoint = NavPoint.FindClosestNavPoint(target, this.AllNavPoints);
        //Debug.Log(gameObject.name + " is dreaming of getting to " + TargetNavPoint.name);

        NavPoint from = LastNavPoint;
        NavPoint to = TargetNavPoint;
        NavPointPath BestPath = from.GetBestPath(to, this.AllNavPoints);
		
		
        return BestPath;
    }
}
