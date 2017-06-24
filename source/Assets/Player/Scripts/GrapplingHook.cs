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
	
	//Prevent hook from traveling forever
	public float maxDistance;
	
	//Grappling Hook gameObject
	public GameObject hook;
	//Grappling Hook prefab
	public GameObject hookPrefab;
	
	public bool retract;
	public bool cancel;
	
	void OnEnable()
	{
		rotation1 = transform.GetChild(0).transform.rotation;
		localPosition1 = transform.GetChild(0).transform.localPosition;
		
		rotation1Sub = transform.GetChild(0).GetChild(0).transform.rotation;
		position1Sub = transform.GetChild(0).GetChild(0).transform.position;
		localPosition1Sub = transform.GetChild(0).GetChild(0).transform.localPosition;
		
		retract = false;
		cancel = false;
		
		hook = null;
	}
	void LateUpdate()
	{
		if(hook == null)
		{
			//Create a new hook from the prefab if not already in existence
			if(hookPrefab == null){
				print("hook prefab unassigned");
			}
			else{
				Vector3 spawnPos = new Vector3(0,0,0);
				hook = Instantiate(hookPrefab, spawnPos,Quaternion.identity);
				hook.GetComponent<HookFly>().grapplingHook = this;
				
				//Ignore grappling hook collisions with player
				Physics2D.IgnoreCollision(hook.GetComponent<Collider2D>(), transform.parent.parent.GetComponent<Collider2D>());
			}
		}
		if(Input.GetMouseButtonDown(0)){
				//Mouse pressed
				DisconnectWithSpring();
				FireHook(5.0f);
				PointShoulderToHook();
				ConnectWithChain();
		}
		else{
		
			if(Input.GetMouseButton(0)){
				//Mouse held down
				PointShoulderToHook();
				
				//Check if max distance has been exceeded
				if(maxDistance < DistanceToHook())
				{
					cancel = true;
				}
				
				//If retracting, connect spring to hook
				//If canceled,stop firing hook
				//Otherwise, keep firing hook
				if(retract){
				ConnectWithSpring();
				ConnectWithChain();
				}else{
				if(cancel){
				//Shooting hook canceled
				PointShoulderToMouse();
				RetractHook();
				AttachForearm();
				
				DisconnectWithSpring();
				DisconnnectWithChain();
				
				//Disable hook object
				hook.GetComponent<BoxCollider2D>().enabled = false;
				hook.GetComponent<HookFly>().enabled = false;
				hook.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				}
				else{
				DisconnectWithSpring();
				ConnectWithChain();
				}
				}
				
			}
			else{
				//Mouse not held down
				PointShoulderToMouse();
				RetractHook();
				AttachForearm();
				
				retract = false;
				cancel = false;
				
				DisconnectWithSpring();
				DisconnnectWithChain();
			}
		}
		SetProperties();
	}
	//List of functions not entirely enclusive
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
		
			Vector3 direction;
			direction = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			direction -= transform.GetChild(0).transform.position;
			
			float angle = Mathf.Atan2(direction.y, direction.x);
			
			Vector2 velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
			
			//Set properties of hook to properties of forearm
			hook.transform.position = position1Sub;
			hook.transform.rotation = rotation1Sub;
			
			//Enable script and set velocity
			hook.GetComponent<HookFly>().enabled = true;
			hook.GetComponent<Rigidbody2D>().velocity = velocity;
			
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
		
		//float scale = playerPhysics.transform.localScale.x;
		
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
	float DistanceToHook()
	{
		Vector3 distance = hook.transform.position - transform.GetChild(0).position;
		return distance.magnitude;
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