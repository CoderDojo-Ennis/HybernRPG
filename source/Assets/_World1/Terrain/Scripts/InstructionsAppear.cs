using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsAppear : MonoBehaviour {

	Transform camera;
	bool showText;
	
	void Start ()
	{
			camera = GameObject.Find( "Main Camera").transform;
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
			Vector3 offset = new Vector3(1, 0, 0);
			transform.position = new Vector3(camera.position.x, camera.position.y, 0) + offset;
		}
	}
	IEnumerator AnimateText()
	{
		yield return new WaitForSeconds(2);
		
		showText = true;
		
		yield return new WaitForSeconds(5);
		
		showText = false;
	}
	
}
