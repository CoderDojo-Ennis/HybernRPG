using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParameterTrigger : MonoBehaviour {

	public string parameter;
	public int value;
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
			GetComponent<Animator>().SetInteger(parameter, value);
			
			Destroy(this);
		}
		
	}
}
