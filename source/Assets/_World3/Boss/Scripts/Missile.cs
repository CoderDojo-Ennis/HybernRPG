using UnityEngine;

public class Missile : MonoBehaviour {

	public GameObject explosion;
    private AudioManager audio;
	void OnEnable()
	{
		Collider2D missile;
		Collider2D roof;
		
		missile = GetComponent<Collider2D>();
		roof = GameObject.Find("roof").GetComponent<Collider2D>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
		
		Physics2D.IgnoreCollision(roof, missile, true);
	}
	void OnCollisionEnter2D()
	{
		Destroy(gameObject);
	}
	void OnDestroy()
	{
        audio.Play("Boom");
        Instantiate(explosion, transform.position, Quaternion.identity);
	}
}
