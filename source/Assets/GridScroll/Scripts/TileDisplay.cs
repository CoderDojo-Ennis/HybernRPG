using UnityEngine;
using System.Collections;

public class TileDisplay : MonoBehaviour {

	//Notes: Sprites imported must have a pixel to unit ration of one.
	//Notes: Sprites import must be set to top left pivot settings.
	//Notes: Sprites import must have point (no filter.)
	
	//Width and height represent the width and height of the sprite on
	//screen. No matter the size, the sprite will be squeezed and
	//stretched to fit within it. These variables are open for other
	//scripts to access during run time.
	//Also handeled here is the assignment of a sprite to the sprite
	//renderer, done using the SpriteStorage script attatched to the
	//SpriteStorage GameObject, which contains the key for sprites
	//based on the index to an array.
	public float width;
	public float height;
	public int spriteIndex;
	public bool show;
	
	void Start()
	{
		Refresh();
	}
	void Update()
	{
		//Refresh();
	}
	public void Refresh()
	{
		//Select sprite from file based on value of spriteIndex.
		GameObject storage = GameObject.Find("SpriteStorage");
		GetComponent<SpriteRenderer>().sprite = storage.GetComponent<SpriteStorage>().Sprite(spriteIndex);
		//Set up scale of sprite.
		float sizeX = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		float sizeY = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
		Vector3 scale = new Vector3(width/sizeX, height/sizeY, 1);
		GetComponent<Transform>().localScale = scale;
		//If show is true, then show, otherwise hide.
		GetComponent<SpriteRenderer>().enabled = show;
	}
}
