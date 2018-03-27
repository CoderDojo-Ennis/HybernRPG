using UnityEngine;

public class Close : MonoBehaviour
{
	public ChangeLimb changeLimb;
	void Start ()
	{
		changeLimb = GameObject.Find("Player Physics Parent").GetComponent<ChangeLimb>();
	}
	
	public void CloseMenu ()
	{
		changeLimb.WheelControl();
	}
}
