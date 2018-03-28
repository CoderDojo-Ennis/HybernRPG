using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
	public GameObject explosion;
	public GameObject WarningPrefab;
	private AudioManager audioMan;
	private GameObject Warning;
	public LayerMask layers;
	public float DistanceToSpawnWarning;
	void OnEnable()
	{
		Collider2D missile;
		Collider2D roof;

		missile = GetComponent<Collider2D>();
		roof = GameObject.Find("roof").GetComponent<Collider2D>();
		audioMan = GameObject.Find("AudioManager").GetComponent<AudioManager>();


		Physics2D.IgnoreCollision(roof, missile, true);
		StartCoroutine("SpawnWarning");
	}

	IEnumerator SpawnWarning()
	{
		yield return new WaitForSeconds(0.1f);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, DistanceToSpawnWarning, layers);
		if (hit.collider != null)
		{
			Warning = Instantiate(WarningPrefab, hit.point, Quaternion.identity);
		}
		else
		{
			StartCoroutine("SpawnWarning");
		}
	}

	void OnCollisionEnter2D()
	{
		audioMan.Play("Boom");
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	
	void OnDestroy()
	{
		if (Warning != null)
		{
			Destroy(Warning);
		}
	}
}
