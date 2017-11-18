using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UberCultistBehaviour : MonoBehaviour {
	
	public State state;
	public GameObject missileDroppers;
	public GameObject missile;
	public GameObject worldControl;
	public PlayerStats playerStats;

    private int health = 2;
	private int maxHealth = 50;
	private Slider healthSlider;
    private AudioManager audioManager;
    private CircleCollider2D forceField;
	private SpriteRenderer forceFieldSprite;
	private GameObject player;
	
	public GameObject explosion;
	
	public enum State {
		Axe,
		Laser,
		AirStrike,
		Charge
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
			if((player.transform.position - transform.position).magnitude < 3 && health < 25)
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
	}
	void AirStrike()
	{
		foreach(Transform child in missileDroppers.transform)
		{
			Instantiate(missile, child.transform.position, Quaternion.identity);
		}
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		healthSlider.value = (float)health/maxHealth;
		if(health <= 0)
		{
			this.Delay( 6, () => {
                worldControl.GetComponent<WorldControl>().NextScene();
            });
			
			StartCoroutine( BlowUp () );
		}
	}

	IEnumerator Control ()
	{
		while (true)
		{
			playerStats.shielded = false;
			state = State.Axe;
			yield return new WaitForSeconds(15);
            audioManager.Play("Air Siren");
            yield return new WaitForSeconds(5);
            state = State.AirStrike;
			yield return new WaitForSeconds(1);
			AirStrike();
			yield return new WaitForSeconds(5);
		}
	}
	IEnumerator BlowUp ()
	{
		for(int i = 0; i < 10; i++)
		{
			Vector3 offset;
			offset = new Vector3(Random.Range(-1f, 0.1f), Random.Range(-1, 1) , 0);
			offset += new Vector3(0,1,0);
			
			GameObject clone = Instantiate(explosion, transform.position + offset, Quaternion.identity);
			clone.transform.parent = this.transform;
			
			yield return new WaitForSeconds(0.5f);
		}
		Destroy(this.gameObject);
	}
}
