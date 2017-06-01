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
	}

    public void Attack()
    {
    }

    void Update()
    {
		Vector3 distance = this.transform.position - Player.transform.position;
        if (distance.sqrMagnitude < maxSenseDistance)
        {
            
			if(Random.Range(0.0f, 1.0f) < 0.1f)
			{
				ProjectileAttack();
				print("One more");
			}
        }
    }

    public void TakeDamage()
	{
	}
	
	//Ranged attack affected by gravity
    void ProjectileAttack()
	{
		float distance = Vector3.Distance(transform.position, Player.transform.position);
        GameObject projectile = Instantiate(Projectile, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.AngleAxis(45 + Random.Range(40, 60), Vector3.up));
		projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
    }

}
