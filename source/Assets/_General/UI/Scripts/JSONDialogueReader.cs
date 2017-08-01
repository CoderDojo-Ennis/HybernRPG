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
	private PlayerStats playerStats;

    public Button ContinueButton;
    public GameObject DialogueTextUI;
    public GameObject SpeakerTextUI;
    
	void Start() 
    {
        //DialogueTextUI.transform.parent.gameObject.SetActive(false);
        playerStats = GameObject.Find( "Player Physics Parent").GetComponent< PlayerStats >();
		
		ContinueButton.GetComponent<Button>().onClick.AddListener(ContinueButtonFunction);
		
        //Debug
        //Debug.Log(GetText("Player", "0"));
        //DisplayDialogue("Cultist", "0"); // Latest DisplayDialogue called appears.
		
		
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
		if(id == "exit")
		{
			DialogueTextUI.transform.parent.gameObject.SetActive(false);
			UnPause();
			return;
		}
		
        DisplaySpeaker = speaker;
        DisplayID = id;
        if (!DialogueTextUI.transform.parent.gameObject.activeSelf)
        {
            DialogueTextUI.transform.parent.gameObject.SetActive(true);
            DialogueTextUI.SetActive(true);
        }
        //display text
		StopAllCoroutines();
		StartCoroutine( "PrintText" );
		
        SpeakerTextUI.GetComponent<Text>().text = speaker;
    }
    string GetNext(string speaker, string id) //Searches dialogue.json for the next piece of text in a conversation. Can likely be debugged in Start().
    {
        for (int i = 0; i < dialogueData[speaker].Count; i++)
        {
            if (dialogueData[speaker][i]["id"].ToString() == id)
            {
                if (dialogueData[speaker][i]["next"] != null)
                {
                    return dialogueData[speaker][i]["next"][0]["id"].ToString();
                }
            }
        }
        return "FAIL";
    }
    public void ContinueButtonFunction ()
    {
        
        if (GetNext(DisplaySpeaker, DisplayID) != "FAIL")
        {
			DisplayDialogue (DisplaySpeaker, GetNext(DisplaySpeaker, DisplayID));
        }
        else
        {
            DialogueTextUI.SetActive(false);
        }
    }
	public void BeginDialogue (string fileName, string speaker, string id)
	{
		Pause();
		
		textData = File.ReadAllText(Application.dataPath + "/_World1/JSON/" + fileName + ".json");
        dialogueData = JsonMapper.ToObject(textData);
        
		DisplayDialogue (speaker, id);
	}
	void Pause()
	{
		playerStats.paused = true;
	}
	void UnPause()
	{
		playerStats.paused = false;
	}
	IEnumerator PrintText ()
	{
		//Store text characters in an array
		char[] characters = GetText(DisplaySpeaker, DisplayID).ToCharArray();
		//Empty text box
		DialogueTextUI.GetComponent<Text>().text = null;
		
		//Cycle through text and transfer it to text box
		for( int counter = 0; counter < characters.Length; counter++)
		{
			
			DialogueTextUI.GetComponent<Text>().text += characters[counter];
			
			GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("TextProgression");
			
			yield return null;
		}
	}
}
