using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

	//Shoulder1 properties
	private Quaternion rotation1;
	private Vector3 localPosition1;
	
	//Forearm1 properties (acting as hook)
	private Quaternion rotation1Sub;
	private Vector3 position1Sub;
	private Vector3 localPosition1Sub;
	
	//Grappling Hook gameObject
	public GameObject hook;
	
	public bool retract;
	
	public float output;
	public float Angle;
	
	void OnEnable()
	{
		rotation1 = transform.GetChild(0).transform.rotation;
		localPosition1 = transform.GetChild(0).transform.localPosition;
		
		rotation1Sub = transform.GetChild(0).GetChild(0).transform.rotation;
		position1Sub = transform.GetChild(0).GetChild(0).transform.position;
		localPosition1Sub = transform.GetChild(0).GetChild(0).transform.localPosition;
		
		hook.GetComponent<HookFly>().grapplingHook = this;
		
		retract = false;
		
		//Ignore grappling hook collisions with player
		Physics2D.IgnoreCollision(hook.GetComponent<Collider2D>(), transform.parent.parent.GetComponent<Collider2D>());
	}
	void LateUpdate()
	{
		if(Input.GetMouseButtonDown(0)){
				//Mouse pressed
				DisconnectWithSpring();
				FireHook(5);
				PointShoulderToHook();
				ConnectWithChain();
		}
		else{
		
			if(Input.GetMouseButton(0)){
				//Mouse held down
				PointShoulderToHook();
				
				if(retract){
				ConnectWithSpring();
				}else{
				DisconnectWithSpring();
				}
				ConnectWithChain();
			}
			else{
				//Mouse not held down
				PointShoulderToMouse();
				RetractHook();
				AttachForearm();
				
				retract = false;
				//RecallHook();
				DisconnectWithSpring();
				DisconnnectWithChain();
			}
		}
		Angle += 0.01f;

		Debug.DrawLine(new Vector3(0,0,0), new Vector3(20 * Mathf.Cos(Angle),20 * Mathf.Sin(Angle),0));
			
		SetProperties();
	}/*
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
	}*/
	//List of functions
	//void FireHook();
	//void PointShoulderToHook();
	//void PointShoulderToMouse();
	//void PointShoulderTo(Vector2 target);
	//void AttachForearm();
	//void RetractHook();
	
	void FireHook(float speed)
	{
			//Find physics object
			GameObject playerPhysics;
			playerPhysics = transform.parent.parent.gameObject;
		
			Vector3 mousePos;
			mousePos= Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			mousePos = new Vector3(mousePos.x, mousePos.y);
			
			Vector3 pointTo;
			pointTo = mousePos - transform.GetChild(0).transform.position;
			
			float angle = Mathf.Atan2(pointTo.y, pointTo.x) * Mathf.Rad2Deg;
			
			Vector2 velocity = Quaternion.EulerAngles(0,0, angle) * new Vector2(speed, 0);//new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
			
			//Set properties of hook to properties of forearm
			hook.transform.position = position1Sub;
			hook.transform.rotation = rotation1Sub;
			
			//Enable script and set velocity
			hook.GetComponent<HookFly>().enabled = true;
			hook.GetComponent<Rigidbody2D>().velocity = velocity;//hook.GetComponent<Rigidbody2D>().velocity.normalized;
			//hook.GetComponent<Rigidbody2D>().velocity.Normalize();
		
			
			//hook.GetComponent<Rigidbody2D>().velocity += new Vector2(0, playerPhysics.GetComponent<Rigidbody2D>().velocity.y);
			
			//hide forearm not being used as hook
			transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			//Show hook in it's place
			hook.GetComponent<SpriteRenderer>().enabled = true;
	}
	void PointShoulderToHook()
	{
		//point to hook
		PointShoulderTo(hook.transform.position);
	}
	void PointShoulderToMouse()
	{
		PointShoulderTo(Camera.main.ScreenToWorldPoint (Input.mousePosition));
	}
	void PointShoulderTo(Vector3 target)
	{
		target = target - transform.GetChild(0).transform.position;
		float angle;
		angle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;
		
		//Set rotation variable
		rotation1 = Quaternion.AngleAxis (angle+90, Vector3.forward);
	}
	void AttachForearm()
	{
		Vector3 scale;
		scale = new Vector3(transform.parent.parent.localScale.x * -1, 1, 1);
		
		Vector3 position;
		position = rotation1 * localPosition1Sub;
		
		position = Vector3.Scale(position, scale);
		position1Sub = transform.GetChild(0).transform.TransformPoint(position);
		rotation1Sub = rotation1;
		
		//Show forearm
		transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		//Hide hook
		hook.GetComponent<SpriteRenderer>().enabled = false;
	}
	void RetractHook()
	{
	}
	void ConnectWithChain()
	{
		//Find physics object
		GameObject playerPhysics;
		playerPhysics = transform.parent.parent.gameObject;
		
		float scale = playerPhysics.transform.localScale.x;
		
		Vector3 armPos;
		Vector3 hookPos;
		armPos = transform.position;
		//armPos += rotation1 * new Vector3(0, -0.17f, 0);//-0.08f * scale
		
		hookPos = hook.transform.position;
		//hookPos +=;
		//Set positions of ends
		hook.GetComponent<LineRenderer>().SetPosition(0, hookPos);
		hook.GetComponent<LineRenderer>().SetPosition(1, armPos);
		
		//Display chain
		hook.GetComponent<LineRenderer>().enabled = true;
	}
	void DisconnnectWithChain()
	{	
		//Hide Chain
		hook.GetComponent<LineRenderer>().enabled = false;
	}
	void ConnectWithSpring()
	{
		//Find physics object
		GameObject playerPhysics;
		playerPhysics = transform.parent.parent.gameObject;
		
		//enable spring
		hook.GetComponent<SpringJoint2D>().connectedBody = playerPhysics.GetComponent<Rigidbody2D>();
		//hook.GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(0,0);
		hook.GetComponent<SpringJoint2D>().enabled = true;
		
		//These are the alternative coordinates for the contact point on the player's collider for the spring
		//x= -0.1003531y=0.5240884
	}
	void DisconnectWithSpring()
	{	
		//Enable spring
		hook.GetComponent<SpringJoint2D>().enabled = false;
	}
	void RecallHook()
	{
		hook.GetComponent<SpringJoint2D>().connectedBody = null;
		hook.GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(transform.position.x, transform.position.y);
		hook.GetComponent<SpringJoint2D>().enabled = true;
	}
	void SetProperties()
	{
		//Set rotation property for shoulder1
		transform.GetChild(0).transform.rotation = rotation1;
		//Set local position property for shoulder1
		///transform.GetChild(0).transform.localPosition = localPosition1;
		//Set rotation property for forearm1
		transform.GetChild(0).GetChild(0).transform.rotation = rotation1Sub;
		//Set global position property for forearm1
		///transform.GetChild(0).GetChild(0).transform.position = position1Sub;
	}
	void SmoothTransition(Quaternion rotation)
	{
		//As aiming is controlled by script, the animtor needs to be told
		//what the last orientation of the arm was before control was handed
		//back to it. Otherwise, the animtor is completly ignorant of anyting
		//done in script and will thus cause the arm to snap to it's next
		//position.
		
		float frame;
		
		var rotXcurve = new AnimationCurve();
		var rotYcurve = new AnimationCurve();
		var rotZcurve = new AnimationCurve();
		var rotWcurve = new AnimationCurve();
		
		rotation = Quaternion.Euler(0, 0, 42);
		
		frame = 0;
		rotXcurve.AddKey(frame, rotation.x);
		rotYcurve.AddKey(frame, rotation.y);
		rotZcurve.AddKey(frame, rotation.z);
		rotWcurve.AddKey(frame, rotation.w);
		
		//frame = 60;
		//rotXcurve.AddKey(frame, rotation.x);
		//rotYcurve.AddKey(frame, rotation.y);
		//rotZcurve.AddKey(frame, rotation.z);
		//rotWcurve.AddKey(frame, rotation.w);
		
		//transitionClip.SetCurve("shoulder1", typeof(Transform), "localRotation.x", rotXcurve);
		//transitionClip.SetCurve("shoulder1", typeof(Transform), "localRotation.y", rotYcurve);
		//transitionClip.SetCurve("shoulder1", typeof(Transform), "localRotation.z", rotZcurve);
		//transitionClip.SetCurve("shoulder1", typeof(Transform), "localRotation.w", rotWcurve);
		
		GetComponent<Animator>().SetInteger("ArmLimbs", 3);
	}
}
/*
//Calculate velocity
			Vector3 velocity;
			velocity = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			velocity = velocity - transform.GetChild(0).transform.position;
			
			float angle;
			angle = Mathf.Atan2 (velocity.y, velocity.x) * Mathf.Rad2Deg;
			
			output = angle;

			velocity = new Vector3(speed * Mathf.Cos(10.1f),speed * Mathf.Sin(10.1f),0);
			//velocity = new Vector3(19.69f,3.507f,0);
*/