using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour {
	
	public string ID;
	public string Speaker;
	
	private JSONDialogueReader reader;
	
	void Start () {
		reader = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Speaker == reader.DisplaySpeaker && ID == reader.DisplayID)
		{
			GetComponent<Animator>().enabled = true;
			
			Destroy(this);
		}
		
	}
}
