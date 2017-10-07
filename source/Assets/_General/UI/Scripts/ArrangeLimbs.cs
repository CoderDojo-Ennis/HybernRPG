using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeLimbs : MonoBehaviour {

	public float radius;
	public int numberOfLimbs;
	public float angleBetweenLimbs;
	
	void Start () {
		numberOfLimbs = CountLimbs();
		angleBetweenLimbs = 365f/(float)numberOfLimbs;
		
		PositionLimbs();
	}
	int CountLimbs()
	{
		int n = 0;
		foreach(Transform child in transform)
		{
			if( child.gameObject.activeSelf )
				n++;
		}
		return n;
	}
	void PositionLimbs()
	{
		float angle;
		angle = angleBetweenLimbs * Mathf.Deg2Rad;
		
		int n = 0;
		foreach(RectTransform child in transform)
		{
			if( child.gameObject.activeSelf )
			{
				child.anchoredPosition = new Vector2( radius * Mathf.Cos( angle * n ), radius * Mathf.Sin( angle * n ) );
				n++;
			}
		}
	}
}
