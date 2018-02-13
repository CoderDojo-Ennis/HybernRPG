using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTextBox2 : MonoBehaviour {

	private JSONDialogueReader dialouge;
	public int worldNumber;
	public string fileName;
	public string speaker;
	public string id;
	
	void OnEnable()
    {
        dialouge = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
    }
	void PlayDialogue2 ()
	{
		dialouge.BeginDialogue(worldNumber, fileName, speaker, id);
	}
}
