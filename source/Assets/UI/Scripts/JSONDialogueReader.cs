using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using LitJson;

public class Dialogue
{
    int ID { get; set; }
    string Text { get; set; }
}


public class JSONDialogueReader : MonoBehaviour {
    private string textData;
    private JsonData dialogueData;

    public GameObject DialogueTextUI;
    public GameObject SpeakerTextUI;
    
	void Start () 
    {
        DialogueTextUI.transform.parent.gameObject.SetActive(false);
        textData = File.ReadAllText(Application.dataPath + "/JSON/dialogue.json");
        dialogueData = JsonMapper.ToObject(textData);
        //Debug
        Debug.Log(GetText("Player", "0"));
        DisplayDialogue("Player", "1"); // Latest DisplayDialogue called appears.

	}

	string GetText (string speaker, string id) //Searches dialogue.json for text. Can be debugged like shown in Start().
    {
        for (int i = 0; i < dialogueData[speaker].Count; i++)
        {
            if (dialogueData[speaker][i]["id"].ToString() == id)
                return dialogueData[speaker][i]["text"].ToString();
        }
            return null;
    }
    void DisplayDialogue (string speaker, string id) //Uses GetText to find the text needed and displays it. Can be called like shown in Start(). 
    {
        if (!DialogueTextUI.transform.parent.gameObject.activeSelf)
        {
            DialogueTextUI.transform.parent.gameObject.SetActive(true);
            DialogueTextUI.SetActive(true);
        }
        DialogueTextUI.GetComponent<Text>().text = GetText(speaker, id);
        SpeakerTextUI.GetComponent<Text>().text = speaker;
    }
}
