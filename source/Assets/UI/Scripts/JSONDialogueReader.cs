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
    private string DisplaySpeaker;
    private string DisplayID;
    private string NextSpeaker;
    private string NextID;

    public Button ContinueButton;
    public GameObject DialogueTextUI;
    public GameObject SpeakerTextUI;
    
	void Start () 
    {
        DialogueTextUI.transform.parent.gameObject.SetActive(false);
        textData = File.ReadAllText(Application.dataPath + "/JSON/dialogue.json");
        dialogueData = JsonMapper.ToObject(textData);
        ContinueButton.GetComponent<Button>().onClick.AddListener(ContinueButtonFunction);
        //Debug
        //Debug.Log(GetText("Player", "0"));
        DisplayDialogue("Player", "0"); // Latest DisplayDialogue called appears.
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
        DisplaySpeaker = speaker;
        DisplayID = id;
        if (!DialogueTextUI.transform.parent.gameObject.activeSelf)
        {
            DialogueTextUI.transform.parent.gameObject.SetActive(true);
            DialogueTextUI.SetActive(true);
        }
        DialogueTextUI.GetComponent<Text>().text = GetText(speaker, id);
        SpeakerTextUI.GetComponent<Text>().text = speaker;
    }
    bool GetNext(string speaker, string id) //Searches dialogue.json for the next piece of text in a conversation. Can likely be debugged in Start().
    {
        for (int i = 0; i < dialogueData[speaker].Count; i++)
        {
            if (dialogueData[speaker][i]["id"].ToString() == id)
            {
                Debug.Log("FUNCTION CALLED");
                if (dialogueData[speaker][i]["next"] != null)
                {
                    Debug.Log("FUNCTION CALLED");
                    return true;
                }
            }
        }
        return false;
    }
    void ContinueButtonFunction ()
    {
        
        if (GetNext(DisplaySpeaker, DisplayID))
        {

        }
        else
        {
            DialogueTextUI.SetActive(false);
        }
    }
}
