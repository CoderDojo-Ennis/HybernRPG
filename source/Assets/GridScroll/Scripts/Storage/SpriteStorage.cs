using UnityEngine;
using System.Collections;

public class SpriteStorage : MonoBehaviour {
	public Sprite[] sprites;
	public Sprite Sprite(int spriteIndex)
	{
		if(spriteIndex >= sprites.Length)
		{
			return null;
		}
		return sprites[spriteIndex];
	}
}
