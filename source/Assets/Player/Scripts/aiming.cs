using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {

	private Quaternion rotation;
	public GameObject ammo;
	// Use this for initialization
	void Start ()
	{
       rotation = transform.GetChild(0).transform.rotation;
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		Vector3 pointTo;
		float a;
		
		//Shoulder 1
		pointTo = mousePos - transform.GetChild(0).transform.position;
		a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		
		rotation = Quaternion.Lerp(rotation ,Quaternion.AngleAxis(a+90, Vector3.forward), 0.1f); //point towards mouse
		transform.GetChild(0).transform.rotation = rotation;
	
		//Shoulder 2
		pointTo = mousePos - transform.GetChild(1).transform.position;
		a = Mathf.Atan2 (pointTo.y, pointTo.x) * Mathf.Rad2Deg;
		
		transform.GetChild(1).transform.rotation = Quaternion.AngleAxis (a+90, Vector3.forward); //point towards mouse
		/*if (a > -90 && a < 90) {
				transform.root.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				transform.root.localScale = new Vector3 (-1f, 1f, 1f);
			}*/
			//Debug.Log (a);
			if (Input.GetMouseButtonUp(0))
			{
				Instantiate(ammo, this.transform.position + Quaternion.AngleAxis (a+90, Vector3.forward) * new Vector3(0, -0.7f, 0), Quaternion.AngleAxis (a+90, Vector3.forward));
			}
	}
}
