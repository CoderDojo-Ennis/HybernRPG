using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCultist : EnemyFramework {
    
	public GameObject navPointContainer;
	private NavPoint[] allNavPoints;
	
	private List<NavPoint> possibleDestinations;
	
	void OnEnable ()
	{
		allNavPoints = navPointContainer.GetComponentsInChildren<NavPoint>();
		FindNewDestination();
	}
	void FindNewDestination ()
	{
		//Find all the navpoints from which the cultist can see
		//the player
		foreach( NavPoint navPoint in allNavPoints )
		{
			Vector3 position;
			Vector3 targetPosition;
			
			position = navPoint.transform.position;
			targetPosition = GameObject.Find( "Player Physics Parent" ).transform.position;
			
			if( CheckVision( position, targetPosition ) )
			{
				//LaserCultist could see the player from this point
				possibleDestinations.Add( navPoint );
			}
		}
		
		
		//Now pick the navpoint in possibleDestinations which is
		//closest to the laser cultist
		NavPoint closest;
		closest = FindClosestDestination();
	}
	
	bool CheckVision ( Vector3 position, Vector3 targetPosition )
	{
		//Position to fire raycast from is slightly above  position of navpoint
		position += new Vector3(0, 0.6f, 0);
		//Fire raycast
		RaycastHit2D ray = Physics2D.Raycast(position, targetPosition - position);
		 
		 if (ray.collider == null)
                return false;
         else
        {
			if (ray.collider.gameObject.name == "Player Physics Parent")
			{
				//Debug.DrawRay(position, targetPosition - position);
				return true;
			}
			else
			{
				return false;
			}
		}
		
	}
	
	NavPoint FindClosestDestination ( )
	{
		if(possibleDestinations.Count == 0)
		return null;
		
		NavPoint closest;
		Vector3 position;
		
		closest = null;
		position = transform.position;
		
		foreach( NavPoint navPoint in possibleDestinations)
		{
			if(closest == null)
			{
				closest = navPoint;
				continue;
			}
			
			//Compute displacement to nav point
			Vector3 displacementNew = position - navPoint.transform.position;
			//Compute displacement to current closest
			Vector3 displacementOld = position - closest.transform.position;
			
			//compare
			if(displacementNew.sqrMagnitude < displacementOld.sqrMagnitude)
			{
				closest = navPoint;
			}
			
		}
		
		return closest;
	}
}
