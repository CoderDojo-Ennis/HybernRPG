using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsAppear : MonoBehaviour {

	Transform cam;
	bool showText;
	
	void Start ()
	{
			cam = GameObject.Find( "Main Camera").transform;
			showText = false;
			
			
	}
	void OnTriggerEnter2D()
	{
		GetComponent<Collider2D>().enabled = false;
		StartCoroutine( "AnimateText" );
	}
	
	void Update()
	{
		GetComponent<SpriteRenderer>().enabled = showText;
		if(showText)
		{
			Vector3 offset = new Vector3(1, 0.5f, 0);
			transform.position = new Vector3(cam.position.x, cam.position.y, 0) + offset;
		}
	}
	IEnumerator AnimateText()
	{
		yield return new WaitForSeconds(2);
		
		showText = true;
		
		yield return new WaitForSeconds(4);
		
		showText = false;
	}
	
}
