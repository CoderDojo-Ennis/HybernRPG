using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxes : MonoBehaviour {

	public bool pickaxesEnabled;
	
	private Quaternion rotation1;
	private Quaternion rotation2;
	
	private Collider2D pickaxe1;
	private Collider2D pickaxe2;
	
	private float offset;
	private bool slicing;
	
	private PlayerStats playerStats;
	
	void OnEnable()
	{
		//Find player's collider
		Collider2D playerCollider;
		playerCollider = transform.parent.parent.GetComponent<Collider2D>();
		
		//Find pickaxe colliders
		pickaxe1 = transform.GetChild(0).GetComponent<Collider2D>();
		pickaxe2 = transform.GetChild(1).GetComponent<Collider2D>();
		
		//Ignore grappling hook collisions with player
		Physics2D.IgnoreCollision(pickaxe1, playerCollider);
		Physics2D.IgnoreCollision(pickaxe2, playerCollider);
		//And between the pickaxes
		Physics2D.IgnoreCollision(pickaxe1, pickaxe2);
		
		offset = 0;
		slicing = false;
		
		pickaxe1.isTrigger = true;
		pickaxe2.isTrigger = true;
		
		pickaxe1.enabled = false;
		pickaxe2.enabled = false;
		
		pickaxesEnabled = false;
		
		playerStats = GameObject.Find("Player Physics Parent").GetComponent<PlayerStats>();
	}
	void OnDisable ()
	{
		pickaxe1.isTrigger = false;
		pickaxe2.isTrigger = false;
		
		pickaxe1.enabled = false;
		pickaxe2.enabled = false;
		pickaxesEnabled = false;
	}
	void LateUpdate ()
	{
		if(!playerStats.paused && Time.timeScale == 1)
		{
			float scale = transform.parent.parent.localScale.x;
			if(scale == 1){
				GetComponent<Animator>().transform.GetChild(0).rotation = Quaternion.Euler(0, 0, PointShoulderToMouse(0) - 180 -offset);
				GetComponent<Animator>().transform.GetChild(1).rotation = Quaternion.Euler(0, 0, PointShoulderToMouse(0) - 180 - offset);
			}
			else{
				GetComponent<Animator>().transform.GetChild(0).rotation = Quaternion.Euler(0, 0, PointShoulderToMouse(0) + offset);
				GetComponent<Animator>().transform.GetChild(1).rotation = Quaternion.Euler(0, 0, PointShoulderToMouse(0) + offset);
			}
			if(Input.GetMouseButtonDown(0))
			{
				slicing = true;
				
				if( offset == 0 )
				GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Slash");
			}
			if(slicing)
			{
				offset -= 750 * Time.deltaTime;
				if(offset < -360){
					offset = 0;
					slicing = false;
				}
				pickaxe2.isTrigger = true;
				pickaxe2.enabled = true;
				pickaxesEnabled = true;
			}
			else
			{
				pickaxe2.isTrigger = false;
				pickaxe2.enabled = false;
				pickaxesEnabled = false;
			}
		}
		
	}
	float PointShoulderToMouse(int child)
	{
		return PointTowards(Camera.main.ScreenToWorldPoint (Input.mousePosition), child);
	}
	float PointTowards(Vector3 target, int child)
	{
		target = target - transform.GetChild(child).transform.position;
		float angle;
		angle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;
		
		return angle;
	}
}
