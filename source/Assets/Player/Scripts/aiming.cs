using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {

	private Quaternion rotation1;
	private Quaternion rotation2;
	private Vector3 position1;
	private Vector3 position2;
	public GameObject ammo;
	public float lerpValue;
	
	// Use this for initialization
	void Start ()
	{
       rotation1 = transform.GetChild(0).transform.rotation;
	   rotation2 = transform.GetChild(1).transform.rotation;
	   
	   position1 = transform.GetChild(0).transform.localPosition;
	   position2 = transform.GetChild(1).transform.localPosition;
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
		//Make sure that lerpValue is greater than
		//zero and less than or equal to 1.
		lerpValue = Mathf.Clamp(lerpValue, 0, 1);
		if(lerpValue == 0)
		{
			lerpValue = 1;
		}
		
		Vector3 mousePos;
		mousePos= Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		
		Vector3 pointTo;
		float a;
		
		//Shoulder 1
		//Calculate displacement vector to mouse position
		pointTo = mousePos - transform.GetChild(0).transform.position;
		a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		a = ClampAngle(a);
		
		//Interpolate towards desired direction
		rotation1 = Quaternion.Lerp(rotation1 ,Quaternion.AngleAxis(a+90, Vector3.forward), lerpValue);
		transform.GetChild(0).transform.rotation = rotation1;
	
		//Shoulder 2
		//Calculate displacement vector to mouse position
		pointTo = mousePos - transform.GetChild(1).transform.position;
		a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		a = ClampAngle(a);
		
		//Interpolate towards desired direction
		rotation2 = Quaternion.Lerp(rotation2 ,Quaternion.AngleAxis(a+90, Vector3.forward), lerpValue);
		transform.GetChild(1).transform.rotation = rotation2;
		
		//Fire arm cannon if mouse clicked
		if (Input.GetMouseButtonUp(0))
		{
			Instantiate(ammo, this.transform.position+ position2 + rotation2 * new Vector3(0, -0.7f, 0), Quaternion.AngleAxis (a+90, Vector3.forward));
		}
	}
	float ClampAngle(float angle)
	{
		//Arm can't point backwards
		if(transform.root.localScale.x == 1)
		{
			//Arm limited to right half of screen
			if(angle > 90)
			{
				angle = 90;
			}
			else
			{
				if(angle < -90)
				{
					angle = -90;
				}
			}
		}
		else
		{
			//Arm limited to left half of screen
			if(angle < 90 && angle > 0)
			{
				angle = 90;
			}
			else
			{
				if(angle > -90 && angle < 0)
				{
					angle = -90;
				}
			}
		}
		return angle;
	}
}
