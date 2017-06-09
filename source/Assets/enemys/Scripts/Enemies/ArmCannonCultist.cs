using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCannonCultist : EnemyFramework {
    public GameObject Player;
    public GameObject Projectile;
    private Vector3 scale;
    private Vector3 position;

    //Sets variables from EnemyFramework
    void OnEnable()
	{
		walkSpeed = 7;
		runSpeed = 5;
		jumpForce = 4;
        maxSenseDistance = 6f;
		health = 10;
		
		//Find player
		Player = GameObject.Find("Player Physics Parent");
	}

    override public void Attack()
    {
    }

    void Update()
    {
		Vector3 distance;
		distance = this.transform.position - Player.transform.position;
        if (distance.sqrMagnitude < maxSenseDistance)
        {
            
			if(Random.Range(0.0f, 1.0f) < 0.1f)
			{
				ProjectileAttack();
			}
        }
    }
	
	//Ranged attack affected by gravity
    void ProjectileAttack()
	{
		//Calculate firing velocity
		Vector2 velocity = Aim((Vector2)transform.position + new Vector2(0,0.5f), (Vector2)Player.transform.position + new Vector2(0,0.5f), 0.5f);
		
		float distance = Vector3.Distance(transform.position, Player.transform.position);
        GameObject projectile = Instantiate(Projectile, transform.position + new Vector3(0,0.5f,0), Quaternion.AngleAxis(45 + Random.Range(40, 60), Vector3.forward));
		projectile.GetComponent<Rigidbody2D>().velocity = velocity;
		//EnemyBlast needs to have the gameObject of the enemy which spawned it assigned to 'creator' in script
		projectile.GetComponent<EnemyBlast>().creator = gameObject;
    }
	Vector2 Aim(Vector2 launch, Vector2 target, float time)
	{
		//Calculates velocity to launce projectile at in order to hit player
		Vector2 velocity;
		Vector2 displacement = target - launch;
		
		Debug.DrawRay(launch, displacement);
		
		velocity.y = displacement.y/time + (9.8f * time)/2;
		velocity.x = displacement.x/time;
		
		return velocity;
	}
}
