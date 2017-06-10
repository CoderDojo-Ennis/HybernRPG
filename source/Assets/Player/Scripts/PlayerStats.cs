using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int health;
	public GameObject menu;
	public CameraFollow cameraFollow;
	
	public void Start()
	{
		health = 8;
		cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
		menu = GameObject.Find("UI").transform.Find("Canvas").gameObject;
	}

	public void TakeDamage(int damage)
	{
		SubtractDamage(damage);
		
		StopAllCoroutines();
		StartCoroutine(cameraFollow.MyRoutine(0.5f, 0.05f, 0.05f));
		
		if(health == 0)
		{
			Die();
		}
	}

	private void Die()
	{
		//Disable animators on child objects
		this.transform.GetChild(0).GetComponent<AnimationControl>().DisableAnimator();
		//Disable rigidBody
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		//Disable capsule collider
		GetComponent<CapsuleCollider2D>().enabled = false;
		
		
		//Add on rigid body components
		Rigidbody2D rb;
		//Head
		rb = this.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.velocity = new Vector2(0,3);
		this.transform.GetChild(0).GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
		//Arms
			//Shoulder1
			rb = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Rigidbody2D>();
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.velocity = new Vector2(-1,1);
			this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
			//Shoulder2
			rb = this.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Rigidbody2D>();
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.velocity = new Vector2(1,1);
			this.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
		//Torso
		rb = this.transform.GetChild(0).GetChild(2).GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.velocity = new Vector2(0,2);
		this.transform.GetChild(0).GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
		//Legs
			//Thigh1
			rb = this.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Rigidbody2D>();
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.velocity = new Vector2(-0.5f,1);
			this.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
			//Thigh2
			rb = this.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Rigidbody2D>();
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.velocity = new Vector2(-0.5f,1);
			this.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
		
		GetComponent<movement>().enabled = false;
		
		//Die() only called when health == 0, and we only want to call Die() once
		health -= 1;
		menu.SetActive(true);
        
	}

	private void SubtractDamage(int damage)
	{
		//A special function is required to take away damage, so that the player's health
		//is only at 0 for a single frame, during which Die() is called. If Die() is called
		//more than once, it creates a strange hovering body parts effect
		if(health > 0 && health - damage < 0)
		{
			health = 0;
		}
		else
		{
			health -= damage;
		}
	}
}
