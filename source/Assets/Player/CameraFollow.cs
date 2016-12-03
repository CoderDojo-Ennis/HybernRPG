using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform Player;
	//public Transform startMarker;
	//public Transform endMarker;
	public float speed = .1F;
	//private float startTime;
	//private float journeyLength;

	// Use this for initialization
	void Start () {
		
	//	startTime = Time.time;
	//	journeyLength = Vector3.Distance (startMarker.position, endMarker.position);
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position = Player.position
	//	float distCovered = (Time.time - startTime) * speed;
	//	float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp (transform.position, Player.position, speed);
		this.transform.Translate (0, 0, -10);
	}
}
