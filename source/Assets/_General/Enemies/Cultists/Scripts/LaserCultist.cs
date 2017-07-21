using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCultist : EnemyFramework {
    
	//laser movement variables
	public float laserAngle;
	[Range(0.0001f, 1)]
	public float armSpeed;
	private Transform arm;
	private Quaternion armRot;
	//laser display variables
	public Material laser;
	public Material deathLaser;
	private float laserWidth;
	private float deathLaserWidth;
	//variable dictating what the laser is doing
	public LaserActions laserAction;
	
	public GameObject navPointContainer;
	private NavPoint[] allNavPoints;
	private List<NavPoint> possibleDestinations;
	
	
	public enum LaserActions
    {
		noBeam,
        searchBeam,
        searchBeamGrowing,
		searchBeamShrinking,
        deathLaser,
		deathLaserGrowing,
        deathLaserShrinking
    }
	
	void Start()
	{
	
		arm = transform.GetChild(0).GetChild(1).GetChild(0);
		
		allNavPoints = navPointContainer.GetComponentsInChildren<NavPoint>();
		FindNewDestination();
		laserWidth = 0;
		deathLaserWidth = 0;
		
		//EnemyFramework variables
		attack = 2;
	}
	void OnEnable()
	{
		laserAction = LaserActions.searchBeamGrowing;
		laserWidth = 0;
		deathLaserWidth = 0;
	}
	void LateUpdate()
	{	
		//Move arm to point in desired angle
		Quaternion rotation = Quaternion.Euler(0, 0, laserAngle);
		armRot = Quaternion.Lerp( armRot, rotation, armSpeed);
		arm.rotation = armRot;
	}
	void FixedUpdate ()
	{
		//Find player
		Vector2 displacement;
		displacement = (GameObject.Find("Player Physics Parent").transform.position + new Vector3(0, 0.5f, 0)) - arm.position;
		laserAngle = Mathf.Atan2 (displacement.y, displacement.x) * Mathf.Rad2Deg;
		laserAngle += 90;
		
		switch ( laserAction )
		{
			case LaserActions.noBeam:
				///Not showing a beam, so disable the line renderer
				DisableLineRenderer();
				laserWidth = 0;
				deathLaserWidth = 0;
			break;
			case LaserActions.searchBeam:
				///Regular red search beam, non lethal
				laserWidth = 0.05f;
				ShowLaser(armRot.eulerAngles.z, laserWidth, laser);
				if(SearchBeam(armRot.eulerAngles.z)){
				StartCoroutine(LaserOfDeath());
				}
			break;
			case LaserActions.searchBeamGrowing:
				///SearchBeam quickly grows to its full size
				if( laserWidth < 0.05f ){
					laserWidth += 0.002f;
					ShowLaser(armRot.eulerAngles.z, laserWidth, laser);
				}
				else{
					laserAction = LaserActions.searchBeam;
				}
			break;
			case LaserActions.searchBeamShrinking:
				///SearchBeam quickly shrinks to nothing
				if( laserWidth > 0.005f ){
					laserWidth -= 0.002f;
					ShowLaser(armRot.eulerAngles.z, laserWidth, laser);
				}
				else{
					laserAction = LaserActions.noBeam;
				}
			break;
			case LaserActions.deathLaser:
				///Extra large deathLaser, which does damage
				deathLaserWidth = 0.2f;
				ShowLaser(armRot.eulerAngles.z, deathLaserWidth, deathLaser);
				//See if player is in the way of beam
				if(SearchBeam(armRot.eulerAngles.z))
				{
					GameObject.Find( "Player Physics Parent" ).GetComponent<PlayerStats>().TakeDamage(attack);
				}
				
				//animate beam
				deathLaser.mainTextureOffset -= new Vector2(10 * Time.deltaTime, 0);
			break;
			case LaserActions.deathLaserGrowing:
				///Death Laser Beam quickly grows to its full size
				if( deathLaserWidth < 0.2f ){
					deathLaserWidth += 0.05f;
					//animate beam
					deathLaser.mainTextureOffset -= new Vector2(10 * Time.deltaTime, 0);
					ShowLaser(armRot.eulerAngles.z, deathLaserWidth, deathLaser);
				}
				else{
					laserAction = LaserActions.deathLaser;
				}
			break;
			case LaserActions.deathLaserShrinking:
				///SearchBeam quickly shrinks to nothing
				if( deathLaserWidth > 0.005f ){
					deathLaserWidth -= 0.005f;
					//animate beam
					deathLaser.mainTextureOffset -= new Vector2(10 * Time.deltaTime, 0);
					ShowLaser(armRot.eulerAngles.z, deathLaserWidth, deathLaser);
				}
				else{
					laserAction = LaserActions.deathLaserShrinking;
				}
			break;
		}
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
	private void Attack()
	{
		float angle;
		angle = armRot.eulerAngles.z -90;
		angle *= Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        //Create position to fire from
		Vector2 origin;
		origin = arm.position;
		origin += new Vector2(0.015f, 0) * transform.localScale.x;//This just makes sure the raycast fires from within the collider
		RaycastHit2D searchBeam = Physics2D.Raycast(origin, direction);
		
		
		origin += (Vector2) ( arm.rotation * new Vector2(0, -0.3f) );
		if(searchBeam)
		{
			//Hit something, show beam
			Debug.DrawLine(origin, searchBeam.point);
			EnableLineRenderer(0.2f, origin, searchBeam.point, deathLaser);
			if(searchBeam.transform.gameObject.tag == "Good")
			{
				searchBeam.transform.gameObject.GetComponent<PlayerStats>().TakeDamage(attack);
			}
		}
		else{
		//Hit nothing, show beam anyway
		EnableLineRenderer(0.2f, origin, direction * 100, deathLaser);
		}
		deathLaser.mainTextureOffset -= new Vector2(10 * Time.deltaTime, 0);
	}
	private bool SearchBeam(float angle)
	{
		//Returns false if player not hit

		//Create vector from angle
		angle -= 90;
		angle *= Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        //Create position to fire from
		Vector2 origin;
		origin = arm.position;
		origin += new Vector2(0.015f, 0) * transform.localScale.x;//This just makes sure the raycast fires from within the collider
		RaycastHit2D searchBeam = Physics2D.Raycast(origin, direction);
		
		if(searchBeam)
		{
			//Hit something, show beam
			Debug.DrawLine(origin, searchBeam.point);
			//EnableLineRenderer(0.05f, origin, searchBeam.point, laser);
			if(searchBeam.transform.gameObject.tag == "Good")
			{
				origin += (Vector2) ( arm.rotation * new Vector2(0, -0.3f) );
				//EnableLineRenderer(0.0f, origin, searchBeam.point, deathLaser);
				return true;
			}
			return false;
		}
		//Hit nothing, show beam anyway
		//EnableLineRenderer(0.05f, origin, direction * 100, laser);
		return false;
	}
	private void EnableLineRenderer(float width, Vector2 origin, Vector2 end, Material material)
	{
		GetComponent<LineRenderer>().widthMultiplier = width;
		GetComponent<LineRenderer>().material = material;
		GetComponent<LineRenderer>().SetPosition(0, origin);
		GetComponent<LineRenderer>().SetPosition(1, end);
		GetComponent<LineRenderer>().sortingLayerName = "BodyFront";
		//GetComponent<LineRenderer>().sortingOrder = -1;
		GetComponent<LineRenderer>().enabled = true;
		
	}
	private void DisableLineRenderer()
	{
		GetComponent<LineRenderer>().enabled = false;
	}
	private IEnumerator LaserOfDeath()
	{
		GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Portal Sound Effect");
		laserAction = LaserActions.searchBeamShrinking;
		yield return new WaitForSeconds(0.5f);
		laserAction = LaserActions.deathLaserGrowing;
		yield return new WaitForSeconds(1f);
		laserAction = LaserActions.deathLaserShrinking;
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("Portal Sound Effect");
		yield return new WaitForSeconds(0.5f);
		laserAction = LaserActions.searchBeamGrowing;
	}
	void ShowLaser(float angle, float width, Material laser)
	{
		angle -= 90;
		angle *= Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
       
	   //Create position to fire from
		Vector2 origin;
		origin = arm.position;
		origin += new Vector2(0.015f, 0) * transform.localScale.x;//This just makes sure the raycast fires from within the collider
		RaycastHit2D searchBeam = Physics2D.Raycast(origin, direction);
		
		//Offset so that the laser begins where the cultist's arm ends
		origin += (Vector2) ( arm.rotation * new Vector2(0, -0.3f) );
		
		if(searchBeam)
		{
			//Hit something, show beam
			Debug.DrawLine(origin, searchBeam.point);
			EnableLineRenderer(width, origin, searchBeam.point, laser);
			
		}
		else//Hit nothing, show beam anyway
		{
			EnableLineRenderer(width, origin, direction * 100, laser);
		}
	}
}
