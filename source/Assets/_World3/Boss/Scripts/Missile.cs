using UnityEngine;

public class Missile : MonoBehaviour
{
	public GameObject explosion;
	private AudioManager audioMan;
	void OnEnable()
	{
		Collider2D missile;
		Collider2D roof;

		missile = GetComponent<Collider2D>();
		roof = GameObject.Find("roof").GetComponent<Collider2D>();
		audioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();


		Physics2D.IgnoreCollision(roof, missile, true);
	}
	void OnCollisionEnter2D()
	{
		audioMan.Play("Boom");
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	/*
	void OnDestroy()
	{
		audioMan.Play("Boom");
		Instantiate(explosion, transform.position, Quaternion.identity);
	}
	*/
}
