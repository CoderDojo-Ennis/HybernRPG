using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {

	private Quaternion rotation1;
	private Quaternion rotation2;
	private Vector3 position1;
	private Vector3 position2;
	public GameObject ammo;
	public float lerpValue;
	
	private float restY1;
	private float restY2;
	private bool recoil;
	private bool finishedRecoil;
	public float recoilOffset;
	public float recoilLerpValue;
	
	
	// Use this for initialization
	void Start ()
	{
       rotation1 = transform.GetChild(0).transform.rotation;
	   rotation2 = transform.GetChild(1).transform.rotation;
	   
	   position1 = transform.GetChild(0).transform.localPosition;
	   position2 = transform.GetChild(1).transform.localPosition;
	   
	   restY1 = transform.GetChild(0).GetChild(0).transform.localPosition.y;
	   restY2 = transform.GetChild(1).GetChild(0).transform.localPosition.y;
	   recoil = false;
	   finishedRecoil = false;
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
		//a = ClampAngle(a);
		
		//Interpolate towards desired direction
		rotation1 = Quaternion.Lerp(rotation1 ,Quaternion.AngleAxis(a+90, Vector3.forward), lerpValue);
		transform.GetChild(0).transform.rotation = rotation1;
	
		//Shoulder 2
		//Calculate displacement vector to mouse position
		pointTo = mousePos - transform.GetChild(1).transform.position;
		a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		//a = ClampAngle(a);
		
		//Interpolate towards desired direction
		rotation2 = Quaternion.Lerp(rotation2 ,Quaternion.AngleAxis(a+90, Vector3.forward), lerpValue);
		transform.GetChild(1).transform.rotation = rotation2;
		
		//Fire arm cannon if mouse clicked
		if (Input.GetMouseButtonUp(0) && !recoil)
		{
			Instantiate(ammo, this.transform.position+ position2 + rotation2 * new Vector3(0, -0.7f, 0), Quaternion.AngleAxis (a+90, Vector3.forward));
			recoil = true;
		}
		if(recoil == true)
		{
			Recoil();
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
	void Recoil()
	{
		//Make sure that recoilLerpValue is greater than
		//zero and less than or equal to 1.
		lerpValue = Mathf.Clamp(recoilLerpValue, 0, 1);
		if(recoilLerpValue == 0)
		{
			recoilLerpValue = 1;
		}
		
		Vector3 position1Child = transform.GetChild(0).GetChild(0).transform.localPosition;
		Vector3 position2Child = transform.GetChild(1).GetChild(0).transform.localPosition;
		
		Vector3 updatedPosition1 = position1Child;
		Vector3 updatedPosition2 = position2Child;
		
		float movement1 = 0;
		float movement2 = 0;
		//Interpolate to desired position
		if(!finishedRecoil)
		{
			movement1 = (recoilOffset + restY1 - updatedPosition1.y) * recoilLerpValue;
			movement2 = (recoilOffset + restY2 - updatedPosition2.y) * recoilLerpValue;
		}
		else
		{
			movement1 = (restY1 -updatedPosition1.y) * recoilLerpValue;
			movement2 = (restY2- updatedPosition2.y) * recoilLerpValue;
		}
		
		updatedPosition1 += new Vector3(0,movement1,0);
		updatedPosition2 += new Vector3(0,movement2,0);
		
		transform.GetChild(0).GetChild(0).transform.localPosition = updatedPosition1;
		transform.GetChild(1).GetChild(0).transform.localPosition = updatedPosition2;
		
		if(Mathf.Abs(recoilOffset + restY1- updatedPosition1.y) < 0.0001)
		{
			finishedRecoil = true;
		}
		if(Mathf.Abs(restY1- updatedPosition1.y) < 0.0001)
		{
			//Set up for new recoil
			recoil = false;
			finishedRecoil = false;
		}
	}
}
