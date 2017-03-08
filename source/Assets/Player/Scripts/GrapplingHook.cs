using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

	private Quaternion rotation;
	public GameObject playerPhysics;
	public GameObject anchor;
	public LineRenderer lineRenderer;
	// Use this for initialization
	void Start ()
	{
       rotation = transform.GetChild(0).transform.rotation;
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(!Input.GetMouseButton(0))
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			Vector3 pointTo;
			float a;
			
			//Shoulder 1
			pointTo = mousePos - transform.GetChild(0).transform.position;
			a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
			
			rotation = Quaternion.AngleAxis (a+90, Vector3.forward);//Points towards mouse
			transform.GetChild(0).transform.rotation = rotation;
			playerPhysics.GetComponent<SpringJoint2D>().enabled = false;
			
			playerPhysics.GetComponent<LineRenderer>().enabled = false;
		}
		else
		{
			Vector3 anchorPos = anchor.transform.position;
			Vector3 pointTo;
			float a;
			
			//Shoulder 1
			pointTo = anchorPos - transform.GetChild(0).transform.position;
			a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
			
			rotation = Quaternion.AngleAxis (a+90, Vector3.forward);//Points towards anchor
			transform.GetChild(0).transform.rotation = rotation;
			
			playerPhysics.GetComponent<SpringJoint2D>().enabled = true;
			
			playerPhysics.GetComponent<LineRenderer>().enabled = true;
			playerPhysics.GetComponent<LineRenderer>().SetPosition(0, transform.position);
			playerPhysics.GetComponent<LineRenderer>().SetPosition(1, anchor.transform.position);
		}
		if (Input.GetMouseButtonDown(0))
		{
			anchor.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			anchor.transform.position = new Vector3(anchor.transform.position.x,anchor.transform.position.y,0);
		}
	}
}
