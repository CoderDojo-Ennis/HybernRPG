using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : MonoBehaviour 
{
	public GameObject ammo;
	public bool fired = false;
	
	void Start ()
	{
		ammo = GameObject.Find("PlayerBlast");
	}
	
	void Update()
	{
		if (Input.GetMouseButtonUp(0) && !fired)
		{
			Instantiate(ammo, this.transform.position + new Vector3(0.5f ,0f ,0f), Quaternion.AngleAxis (90, Vector3.forward));
			fired = true;
		}
	}
}
