using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UberCultistBehaviour : MonoBehaviour {
	
	public State state;
	public GameObject missileDroppers;
	public GameObject missile;
	public GameObject worldControl;
	
	private int health = 100;
	private int maxHealth = 100;
	private Slider healthSlider;
	private CircleCollider2D forceField;
	private SpriteRenderer forceFieldSprite;
	private GameObject player;
	
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
		
		StartCoroutine( Control () );
	}
	void Update () {
		if(state == State.Axe)
		{
			GetComponent<UberCultistAI>().enabled = true;
			GetComponent<UberCultistController>().enabled = true;
			if((player.transform.position - transform.position).magnitude < 3)
			{
				forceField.enabled = false;
				forceFieldSprite.enabled = false;
			}
			else
			{
				forceField.enabled = true;
				forceFieldSprite.enabled = true;
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
		}
	}
	void AirStrike()
	{
		foreach(Transform child in missileDroppers.transform)
		{
			Instantiate( missile, child.transform.position, Quaternion.identity);
		}
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		healthSlider.value = (float)health/maxHealth;
		if(health <= 0)
		{
			worldControl.GetComponent<WorldControl>().SwitchScene(10);
			GameObject.Destroy( gameObject );
		}
	}
	IEnumerator Control ()
	{
		while (true)
		{
			state = State.Axe;
			yield return new WaitForSeconds(20);
			
			state = State.AirStrike;
			yield return new WaitForSeconds(1);
			AirStrike();
			yield return new WaitForSeconds(5);
		}
	}
}
