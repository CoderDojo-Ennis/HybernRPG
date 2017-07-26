using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyFramework {
    
	private float angle;
	private float angleChange;
    private float yScale;
    public Material deathLaser;
    void OnEnable()
	{
        //Sets variables from EnemyFramework
        attack = 1;
		walkSpeed = 7;
		runSpeed = 5;
		jumpForce = 4;
		health = 3;
		//Sets variables from LaserCultist
		angle = 0;
		angleChange = 30;
	}
	void Update()
	{
		SearchBeam(angle);
		if(angle > 210){
			angleChange*= -1;
		}
		if(angle < -30){
			angleChange*= -1;
		}
		angle += angleChange * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if(angle > 90) {
            yScale = -1;
            
        } else {
            yScale = 1;
        }
        transform.localScale = new Vector3(-1, yScale, 1);
        deathLaser.mainTextureOffset -= new Vector2(20 * Time.deltaTime, 0);
    }
	//Default ranged attack in straight line
    override public void Attack()
    {
		
    }
	private bool SearchBeam(float angle)
	{
		//Returns false if player not hit

		//Create vector from angle
		angle *= Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        //Create position to fire from
        Vector3 origin = transform.position + new Vector3(Mathf.Cos(angle + 90 * yScale) * 0.2f, Mathf.Sin(angle + 90 * yScale) * 0.2f, 0); //this probably isnt nessessary for the actual turret costume. For this current costume the laser needed to be offset
		
		RaycastHit2D searchBeam = Physics2D.Raycast(origin, direction);
		
		
		
		if(searchBeam)
		{
			//Hit something, show beam
			Debug.DrawLine(origin, searchBeam.point);
			EnableLineRenderer(0.2f, origin, searchBeam.point);
			if(searchBeam.transform.gameObject.tag == "Good")
			{
				searchBeam.transform.gameObject.GetComponent<PlayerStats>().TakeDamage(attack);
				return true;
			}
			return false;
		}
		//Hit nothing, show beam anyway
		EnableLineRenderer(0.2f, origin, direction * 100);
		return false;
	}
	private void EnableLineRenderer(float width, Vector3 origin, Vector2 end)
	{
		GetComponent<LineRenderer>().widthMultiplier = width;
		GetComponent<LineRenderer>().SetPosition(0, origin);
		GetComponent<LineRenderer>().SetPosition(1, end);
		GetComponent<LineRenderer>().enabled = true;
		
	}
	private void DisableLineRenderer()
	{
		GetComponent<LineRenderer>().enabled = false;
	}
}
