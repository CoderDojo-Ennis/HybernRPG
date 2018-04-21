using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour {
	
	public Sprite defaultImage;
	public Sprite gamepadImage;
	
	private SpriteRenderer spriteRenderer;
	
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	void Update ()
	{
		//Check which instuctions to display
		if( ControllerManager.instance.ControllerConnected )
		{
			spriteRenderer.sprite = gamepadImage;
		}
		else
		{
			spriteRenderer.sprite = defaultImage;
		}
		
	}
}
