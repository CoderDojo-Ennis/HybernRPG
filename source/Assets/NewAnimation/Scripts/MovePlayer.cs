using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Transform>().position += new Vector3(0.005f, 0, 0);
	}
}
