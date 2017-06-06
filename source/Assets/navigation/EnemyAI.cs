using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class EnemyAI : MonoBehaviour {
    private PlatformerCharacter2D Character;
    public GameObject NavPointContainer;
    public Transform Player;
    public Thoughts Thought;

    public NavPoint LastNavPoint;
    public NavPoint TargetNavPoint;

    private NavPoint[] AllNavPoints;

    public enum Thoughts
    {
        Idle,
        WalkLeft,
        WalkRight,
        JumpLeft,
        JumpRight
    }

    private void Awake()
    {
        Thought = Thoughts.Idle;
        Character = GetComponent<PlatformerCharacter2D>();
        AllNavPoints = NavPointContainer.GetComponentsInChildren<NavPoint>();
        LastNavPoint = NavPoint.FindClosestNavPoint(this.transform.position, AllNavPoints);
        Debug.Log(this.name + " starting near " + LastNavPoint.name);
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
        float xVelocity = 0;
        float speed = 0.3f;

        switch (this.Thought)
        {
            case Thoughts.Idle:
                xVelocity = 0;
                break;
            case Thoughts.WalkLeft:
                xVelocity = -speed;
                break;
            case Thoughts.WalkRight:
                xVelocity = speed;
                break;
            case Thoughts.JumpLeft:
                xVelocity = -speed;
                jump = true;
                break;
            case Thoughts.JumpRight:
                xVelocity = speed;
                jump = true;
                break;
        }

        // Pass all parameters to the character control script.
        Character.Move(xVelocity, crouch, jump);

        // Todo - think after landing/*
        if (Thought == Thoughts.JumpLeft)
        {
            Thought = Thoughts.WalkLeft;
        } else if (Thought == Thoughts.JumpRight)
        {
            Thought = Thoughts.WalkRight;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var navPoint = collision.gameObject.GetComponent<NavPoint>();
        if (navPoint != null)
        {
            if (navPoint != LastNavPoint)
            {
                Debug.Log("Hit nav point " + navPoint.gameObject.name);
                LastNavPoint = navPoint;
                Think();
            }
        }
    }

    private void Think()
    {
        NavPointPath path = FindPathToTarget(Player.position);

        Thought = Thoughts.Idle;
        if (path != null && path.Neighbors != null && path.Neighbors.Count > 0)
        {
            // Whats the next action
            var neighbor = path.Neighbors[0];
            var neighborVector = neighbor.NeighborPoint.transform.position - this.transform.position;
            switch (neighbor.TravelType)
            {
                case TravelTypes.Walk:
                    if (neighborVector.x > 0)
                    {
                        Thought = Thoughts.WalkRight;
                    } else
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

        if(Thought == Thoughts.Idle)
        {
            this.Delay(0.25f, Think);
        }

        Debug.Log("Thought = " + Thought);
    }

    private NavPointPath FindPathToTarget(Vector3 target)
    {
        TargetNavPoint = NavPoint.FindClosestNavPoint(target, this.AllNavPoints);
        Debug.Log(gameObject.name + " is dreaming of getting to " + TargetNavPoint.name);

        NavPoint from = LastNavPoint;
        NavPoint to = TargetNavPoint;
        NavPointPath BestPath = from.GetBestPath(to, this.AllNavPoints);
        return BestPath;
    }
}
