using UnityEngine;

public class ForceField : MonoBehaviour {

	private GameObject arms;
	
	void Start ()
	{
		GameObject player = GameObject.Find ("Player Physics Parent" );
		arms = player.transform.GetChild(0).GetChild(1).gameObject;
	}
	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.name == "PlayerBlast(Clone)")
			Destroy (collider.gameObject);
			
		if (collider.gameObject.name == "EnemyBlast(Clone)")
			Destroy (collider.gameObject);
		
		if (collider.gameObject.name == "Missile(Clone)")
			Destroy (collider.gameObject);
			
		if (collider.gameObject.name == "Hook(Clone)")
			arms.GetComponent<GrapplingHook>().cancel = true;
	}
}
