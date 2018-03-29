using UnityEngine;

public class BoatScript : MonoBehaviour
{
	Rigidbody2D rigid;
	BoxCollider2D box;
	Vector2 maxSpeed;
	float speed = 0.01f;

	void Start ()
	{
		box = gameObject.GetComponent<BoxCollider2D>();
		rigid = gameObject.GetComponent<Rigidbody2D>();
		maxSpeed = new Vector2(1, 0);
	}

	void Update()
	{
		rigid.velocity = rigid.velocity + new Vector2(speed, 0);
		if ((rigid.velocity.x >= 1) || (rigid.velocity.x <= -1))
		{
			rigid.velocity = maxSpeed;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Soda")
		{
			speed = -speed;
			maxSpeed = -maxSpeed;
		}
	}
}
