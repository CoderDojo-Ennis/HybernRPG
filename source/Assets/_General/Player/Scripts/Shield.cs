using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	private Transform forearm;
	private Quaternion rotation;
	public float lerpValue;
	private PlayerStats playerStats;
	
	void OnEnable()
	{
		forearm = transform.Find("shoulder2").Find("forearm2");	
		forearm.GetComponent<Collider2D>().enabled = true;
		rotation = transform.Find("shoulder2").rotation;
		
		Collider2D player;
		Collider2D shield;
		
		player = GameObject.Find("Player Physics Parent").GetComponent<Collider2D>();
		shield = forearm.GetComponent<Collider2D>();
		
		playerStats = GameObject.Find("Player Physics Parent").GetComponent<PlayerStats>();
		
		Physics2D.IgnoreCollision(player, shield);
	}
	void OnDisable()
	{
		forearm = transform.Find("shoulder2").Find("forearm2");
		forearm.GetComponent<Collider2D>().enabled = false;
	}
	void LateUpdate()
	{
		if(!playerStats.paused && Time.timeScale == 1)
		{
			///Find desired direction for arm
			Vector3 mousePos;
			mousePos= Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			
			Vector3 pointTo;
			float a;
			//Calculate displacement vector to mouse position
			pointTo = mousePos - transform.GetChild(0).transform.position;
			a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
			
			///Lerp to that direction
			lerpValue = Mathf.Clamp(lerpValue, 0, 1);
			if(lerpValue == 0)
			{
				lerpValue = 1;
			}
			rotation = Quaternion.Lerp(rotation ,Quaternion.AngleAxis(a+90, Vector3.forward), lerpValue);
			transform.Find("shoulder2").rotation = rotation;
            transform.Find("shoulder2").Find("forearm2").localRotation = Quaternion.identity;
		}
	}
}
