using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTexture : MonoBehaviour {

	public int width = 256;
	public int height = 256;
	
	public float xOffset = 10;
	public float yOffset = 20;
	
	public float scale = 30f;
	
	private float zParameter = 0;
	
	void Update()
	{
		Renderer renderer = GetComponent<Renderer>();
		
		float pixelsPerUnit = 10;
		
		width = (int) ( transform.localScale.x * pixelsPerUnit );
		height = (int) ( transform.localScale.y * pixelsPerUnit );
		
		renderer.material.mainTexture = GenerateTexture();
		
		zParameter += Time.timeScale * 0.01f;
	}
	Texture2D GenerateTexture ()
	{
		Texture2D texture = new Texture2D(width, height);
		
		for( int x = 0; x < width; x++)
		{
			for( int y = 0; y < height; y++ )
			{
				Color colour = GenerateColour(x, y); 
				texture.SetPixel(x, y, colour);
			}
		}
		
		texture.Apply();
		return texture;
	}
	Color GenerateColour (int x, int y)
	{
		float xCoord = (float)x / width * scale + xOffset;
		float yCoord = (float)y / height * scale + yOffset;
		
		//Noise function goes here
		float sample = Perlin3D( xCoord, yCoord, zParameter);
		sample = (float) System.Math.Round( (double) sample, 1);
		
		Color colour = new Color(sample, 0, 0);
		return colour;
	}
	float Perlin3D(float x, float y, float z)
	{
		//Get all 3 permutations of coordinates for Perlin Noise function
		float xy = Mathf.PerlinNoise( x, y );
		float yz = Mathf.PerlinNoise( y, z );
		float zx = Mathf.PerlinNoise( z, x );
		
		//And their inverse
		float yx = Mathf.PerlinNoise( y, x );
		float zy = Mathf.PerlinNoise( z, y );
		float xz = Mathf.PerlinNoise( x, z );
		
		return (xy + yz + zx + yx + zy + xz)/6f;
	}
}
