using UnityEngine;

public class ExecutionManagement : MonoBehaviour
{
	public float desiredSpeed;
	public float lerpSpeed;
	
	private Rigidbody2D rb;
	void OnEnable () {
		rb = GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Kinematic;
	}
	
	void Update () {
		Vector2 target;
		target = new Vector2(0, desiredSpeed);
		
		Vector2 result;
		result = Vector2.Lerp(rb.velocity, target, lerpSpeed);
		
		rb.velocity = result;
	}
}
