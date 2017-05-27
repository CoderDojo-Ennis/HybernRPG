using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int health;
	public CameraFollow cameraFollow;
	
	public void Start()
	{
		health = 10;
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		
		StopAllCoroutines();
		StartCoroutine(cameraFollow.MyRoutine(0.5f, 0.05f, 0.05f));
		
		if(health <= 0)
		{
			Die();
		}
	}
	private void Die()
	{
		//Disable animators on child objects
		this.transform.GetChild(0).GetComponent<AnimationControl>().DisableAnimator();
		
		//Add on rigid body components
		//Head
		Rigidbody2D rb = this.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.velocity = new Vector2(0,1);
		this.transform.GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
		
		
		GetComponent<movement>().enabled = false;
		this.enabled = false;
	}
}
