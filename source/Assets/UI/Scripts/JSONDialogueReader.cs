using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Dialogue
{
    int ID { get; set; }
    string Text { get; set; }
}


public class JSONDialogueReader : MonoBehaviour {
    private JsonData dialogueData;

    public GameObject DialogueTextUI;

	void Start () 
    {
        //Dialogue speech = new Dialogue();
        dialogueData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/JSON/dialogue.json"));
        //SearchByID(6);
	}
	/*
    string SearchByID (int id)
    {
        for (int i = 0; i < dialogueData.Count; i++)
        {
            if (id == i)
            {
				i;
                return;
                //Debug.Log(i.ToString());
            }
        }
    }
	*/
	void Update () 
    {
	    
	}
}
