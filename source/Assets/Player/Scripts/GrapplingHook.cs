using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

	private Quaternion rotation1;
	private Vector3 position1;
	private Anchor anchor;
	
	public GameObject playerPhysics;
	public GameObject hook;
	public LineRenderer lineRenderer;
	public float speed;
	public bool retract;
	
	void Start ()
	{
		rotation1 = transform.GetChild(0).transform.rotation;
		position1 = transform.GetChild(0).transform.localPosition;
		anchor = hook.GetComponent<Anchor>();
		anchor.grapplingHook = this;
		retract = false;
	}
	void LateUpdate ()
	{
		position1 = transform.GetChild(0).transform.localPosition;
		
		Vector3 mousePos;
		mousePos= Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		
		Vector3 pointTo;
		float a;
		
		//Shoulder 1
		//Calculate displacement to mouse position
		pointTo = mousePos - transform.GetChild(0).transform.position;
		a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		
		if (Input.GetMouseButtonDown(0))
		{
			//Fire grappling hook
			Vector2 scale = new Vector2(transform.parent.parent.localScale.x * -1, 1);
			
			Vector2 offset;
			Vector2 position;
			
			offset  = new Vector2(0,-1);
			position = position1;
			position  = Vector2.Scale(position,scale);
			position += (Vector2)(rotation1 * offset);
			position += (Vector2)this.transform.position;
			
			anchor.Fire(position, pointTo.normalized * speed);
			//anchor.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//anchor.transform.position = new Vector3(anchor.transform.position.x,anchor.transform.position.y,0);
		}	
		if(!Input.GetMouseButton(0))
		{
			//Not retracting
			retract = false;
			//Apply rotation
			rotation1 = Quaternion.AngleAxis (a+90, Vector3.forward);//Points towards mouse
			transform.GetChild(0).transform.rotation = rotation1;
			
			//Disable hook's chain
			playerPhysics.GetComponent<SpringJoint2D>().enabled = false;
			playerPhysics.GetComponent<LineRenderer>().enabled = false;
		}
		else
		{
			//Calculate displacement to hook
			pointTo = hook.transform.position - transform.GetChild(0).transform.position;
			a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		
			//Apply rotation
			rotation1 = Quaternion.AngleAxis (a+90, Vector3.forward);//Points towards mouse
			transform.GetChild(0).transform.rotation = rotation1;

			//Enable retraction of chain if necessary
			if(retract)
			{
				playerPhysics.GetComponent<SpringJoint2D>().enabled = true;
			}
			
			//Display chain
			playerPhysics.GetComponent<LineRenderer>().enabled = true;
			playerPhysics.GetComponent<LineRenderer>().SetPosition(0, transform.position);
			playerPhysics.GetComponent<LineRenderer>().SetPosition(1, anchor.transform.position);
		}
	}
}
