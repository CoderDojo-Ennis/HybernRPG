using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Transform Player;
	public float lerpSpeed = 0.1f;
	public Vector2 shakeOffset;
	private Vector3 position;
	private Vector2 shake;
	private bool shaking;
	void Start()
	{
		position = transform.position;
		shaking = false;
	}
	void FixedUpdate()
	{
		//Lerp to player's positon
		position = Vector3.Lerp (position, Player.position, lerpSpeed);
		
		transform.position = position;
		
		//Offset based on screen shake parameters.
		this.transform.Translate (shake.x, shake.y, 0);
		//Offset slightly on y axis
		this.transform.Translate (0, 0, -10);
		if(shaking)
		{
			ScreenShake();
		}
	}
	void ScreenShake()
	{
		shake = new Vector2(Random.Range(-shakeOffset.x,shakeOffset.x),Random.Range(-shakeOffset.y, shakeOffset.y));
	}
	public IEnumerator MyRoutine(float length, float xOffset, float yOffset)
	{
		shake = new Vector2(xOffset, yOffset);
		shaking = true;
		yield return new WaitForSeconds(length);
		shaking = false;
		shake = new Vector2(0,0);
	}
}
