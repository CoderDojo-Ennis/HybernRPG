using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class JSONDialogueReader : MonoBehaviour {
    private JsonData dialogueData;

	void Start () 
    {
        dialogueData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/JSON/dialogue.json"));
	}
	
	void Update () 
    {
	    
	}
}
