using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager{

	public GameObject gameObject; //The entity firing the laser
	
	public Vector2 lineRendererOffset;
	private float width;
	private Material material;
	private LineRenderer line;
	
	public LaserManager ()
	{
		lineRendererOffset = new Vector2(0,0);
	}
	public GameObject FireLaser (Vector2 origin, float angle, float _width, Material _material)
	{
		//Assign line renderer properties
		width = _width;
		material = _material;
		
		//Access line renderer
		line = gameObject.GetComponent<LineRenderer>();
		line.widthMultiplier = width;
		line.material = material;
		line.enabled = true;
		
		line.positionCount = 1;
		line.SetPosition(0, origin);
		
		//Find direction of raycast
		angle *= Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		
		return ExtendedRaycast ( origin, direction);
	}
	GameObject ExtendedRaycast(Vector2 origin, Vector2 direction)
	{
		RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction);
		Vector2 reflectedDirection = ReflectVector2D (direction, raycastHit.normal);
		
		if(!raycastHit)
		{
			Debug.DrawLine(origin, origin + direction * 100);
			line.positionCount += 1;
			line.SetPosition( line.positionCount - 1, origin + direction * 100);
			gameObject.transform.GetChild(1).gameObject.SetActive( false );
			return null;
		}
		
		Debug.DrawLine(origin, raycastHit.point);
		line.positionCount += 1;
		line.SetPosition( line.positionCount - 1, raycastHit.point); 		
		
		gameObject.transform.GetChild(1).gameObject.SetActive( true );
		gameObject.transform.GetChild(1).position = raycastHit.point;
		gameObject.transform.GetChild(1).localScale = new Vector3(Mathf.Clamp(width * 20f, 0, 2), Mathf.Clamp(width * 20f, 0, 2 ), 1);
		
		return raycastHit.collider.gameObject;
		
		//Removed laser reflection abilities
		/*
		if(raycastHit.collider.gameObject.tag == "Reflect Lasers")
			//return ExtendedRaycast( raycastHit.point,  reflectedDirection);
		else
			return raycastHit.collider.gameObject;
			*/
	}
	Vector2 ReflectVector2D ( Vector2 direction, Vector2 normal )
	{
		return direction - 2 * Vector2.Dot(direction, normal) * normal;	
	}
}
