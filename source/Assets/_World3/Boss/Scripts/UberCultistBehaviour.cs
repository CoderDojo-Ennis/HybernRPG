using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UberCultistBehaviour : MonoBehaviour {
	
	public State state;
	public GameObject missile;
	public GameObject worldControl;
	public GameObject explosion;
	public PlayerStats playerStats;
	public Transform MissileSpawnBoundaryLeft;
	public Transform MissileSpawnBoundaryRight;

    private int health = 100;
	private int maxHealth = 100;
	private Slider healthSlider;
    private AudioManager audioManager;
    private CircleCollider2D forceField;
	private SpriteRenderer forceFieldSprite;
	private GameObject player;
	
	
	public enum State {
		Axe,
		Laser,
		AirStrike,
		Charge,
		Defeated
	}
	
	void Start () {
		healthSlider = GameObject.Find("BossHealth").GetComponent<Slider>();
		state = State.Axe;
		forceField = GetComponent<CircleCollider2D>();
		forceFieldSprite = transform.Find("ForceField").gameObject.GetComponent<SpriteRenderer>();
		player = GameObject.Find("Player Physics Parent");
		playerStats = player.GetComponent<PlayerStats>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		StartCoroutine(Control());
	}
	
	void Update () {
		if(state == State.Axe)
		{
			GetComponent<UberCultistAI>().enabled = true;
			GetComponent<UberCultistController>().enabled = true;
			if((player.transform.position - transform.position).magnitude <= 5 && health <= 70)
			{
				forceField.enabled = true;
                forceFieldSprite.enabled = true;
            }
			if((player.transform.position - transform.position).magnitude >= 5 && health <= 75)
			{
				forceField.enabled = true;
                forceFieldSprite.enabled = true;
            }
			else
			{
				forceField.enabled = false;
				forceFieldSprite.enabled = false;
			}
		}
		else
		{
			GetComponent<UberCultistAI>().enabled = false;
			GetComponent<UberCultistController>().enabled = false;
		}
		
		if( state == State.AirStrike)
		{
			forceField.enabled = true;
			forceFieldSprite.enabled = true;
			if((player.transform.position - transform.position).magnitude < 2)
			{
				playerStats.shielded = true;
            }
			
			if((player.transform.position - transform.position).magnitude > 2)
			{
				playerStats.shielded = false;
            }
		}
		if( state == State.Defeated )
		{
			forceField.enabled = false;
			forceFieldSprite.enabled = false;
			
			GetComponent<UberCultistAI>().enabled = false;
			GetComponent<UberCultistController>().enabled = false;
		}
	}
	
	void AirStrike()
	{
		//Static air strike
		/*
		for (int i = 0; i < 11; i++)
		{ 
			int x = (i * 4)-26;
			int y = 5*((i - 5) * (i - 5))+20;
			Vector2 pos = new Vector2(x, y);
			Instantiate(missile, pos, Quaternion.identity);
		}
		*/
		//Random air strike
		int missileAmount = Random.Range(10, 20);
		for (int i = 0; i < missileAmount; i++)
		{
			float x = Random.Range(MissileSpawnBoundaryLeft.position.x, MissileSpawnBoundaryRight.position.x);
			float y = Random.Range(MissileSpawnBoundaryLeft.position.y, MissileSpawnBoundaryRight.position.y);
			Instantiate(missile, new Vector2(x, y), Quaternion.identity);
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		healthSlider.value = (float)health/maxHealth;
		if(health <= 0)
		{	
			StartCoroutine(BlowUp());
		}
	}

	IEnumerator Control ()
	{
		while (true)
		{
			playerStats.shielded = false;
			if( state != State.Defeated )
			state = State.Axe;
			
			yield return new WaitForSeconds(15);
            if( state != State.Defeated )
			audioManager.Play("Air Siren");
            
			yield return new WaitForSeconds(5);
            if( state != State.Defeated )
			state = State.AirStrike;
			
			yield return new WaitForSeconds(1);
			if( state != State.Defeated )
			AirStrike();
			
			yield return new WaitForSeconds(5);
		}
	}
	IEnumerator BlowUp ()
	{
		//Stop music
		GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = null;
		
		//Stop moving
		state = State.Defeated;
		
		for(int i = 0; i < 10; i++)
		{
			//Random offset for explosion
			Vector3 offset;
			offset = new Vector3(Random.Range(-1f, 0.1f), Random.Range(-1, 1) , 0);
			offset += new Vector3(0,1,0);
			
			//Spawn explosion
			GameObject clone = Instantiate(explosion, transform.position + offset, Quaternion.identity);
			clone.transform.parent = transform;
			
			//Wait
			yield return new WaitForSeconds(0.5f);
		}
		//Onwards!!!
		worldControl.GetComponent<WorldControl>().NextScene();
		
		//Get rid of Uber Cultist
		Destroy(gameObject);
	}
}
