using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxes : MonoBehaviour {

	private Quaternion rotation1;
	private Quaternion rotation2;
	
	private float offset;
	private bool slicing;
	
	void OnEnable()
	{
		//Ignore grappling hook collisions with player
		Physics2D.IgnoreCollision(transform.GetChild(0).GetComponent<Collider2D>(), transform.parent.parent.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(transform.GetChild(1).GetComponent<Collider2D>(), transform.parent.parent.GetComponent<Collider2D>());
		
		offset = 0;
		slicing = false;
	}
	
	void LateUpdate ()
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
		}
		if(slicing)
		{
			offset += 10;
			if(offset == 180){
				offset = 0;
				slicing = false;
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
