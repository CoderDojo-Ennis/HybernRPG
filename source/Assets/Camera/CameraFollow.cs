using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Transform Player;
	public float lerpSpeed = 0.1f;
	
	void FixedUpdate ()
	{
		//Lerp to player's positon
		transform.position = Vector3.Lerp (transform.position, Player.position, lerpSpeed);
		//Offset slightly on y axis
		this.transform.Translate (0, 0, -10);
	}
}
