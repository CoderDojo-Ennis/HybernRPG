using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using LitJson;

public class JSONDialogueReader : MonoBehaviour
{
    private string textData;
    private JsonData dialogueData;
	private GameObject healthDisplay;
    public string DisplaySpeaker;
    public string DisplayID;
    private string NextSpeaker;
    private string NextID;
	private PlayerStats playerStats;

    public Button ContinueButton;
    public GameObject DialogueTextUI;
    public GameObject SpeakerTextUI;
    public bool talking;
    public bool shootConnor;

    void Start() 
    {
        shootConnor = false;
        if (GameObject.Find("Player Physics Parent"))
		{
			playerStats = GameObject.Find("Player Physics Parent").GetComponent<PlayerStats>();
		}
		ContinueButton.GetComponent<Button>().onClick.AddListener(ContinueButtonFunction);
	}

	string GetText (string speaker, string id) //Searches dialogue.json for text.
    {
        for (int i = 0; i < dialogueData[speaker].Count; i++)
        {
            if (dialogueData[speaker][i]["id"].ToString() == id)
                return dialogueData[speaker][i]["text"].ToString();
        }
        return null;
    }

	IEnumerator startShootConnor()
	{
		yield return new WaitForEndOfFrame();
		shootConnor = true;
	}

	void DisplayDialogue (string speaker, string id) //Uses GetText to find the text needed and displays it.
    {
		ContinueButton.Select();
        talking = true;
        if (id == "exit")
		{
			StartCoroutine("startShootConnor");
            //shootConnor = true;
            talking = false;
			//Show health display again
			if (healthDisplay != null)
			{
				healthDisplay.SetActive(true);
			}
		
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
		StartCoroutine("PrintText");
		
        SpeakerTextUI.GetComponent<Text>().text = speaker;
    }

    string GetNextID(string speaker, string id) //Searches dialogue.json for the next piece of text in a conversation
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

	string GetNextSpeaker(string speaker, string id) //Searches dialogue.json for the next piece of text in a conversation.
    {
        for (int i = 0; i < dialogueData[speaker].Count; i++)
        {
            if (dialogueData[speaker][i]["id"].ToString() == id)
            {
                if (dialogueData[speaker][i]["next"] != null)
                {
                    return dialogueData[speaker][i]["next"][0]["speaker"].ToString();
                }
            }
        }
        return "FAIL";
    }

    public void ContinueButtonFunction ()
    {
        if (GetNextID(DisplaySpeaker, DisplayID) != "FAIL" && GetNextSpeaker(DisplaySpeaker, DisplayID) != "FAIL")
        {
			DisplayDialogue (GetNextSpeaker(DisplaySpeaker, DisplayID), GetNextID(DisplaySpeaker, DisplayID));
        }
        else
        {
            DialogueTextUI.SetActive(false);
        }
    }

	public void BeginDialogue (int worldNumber, string fileName, string speaker, string id)
	{
		Pause();
		
		TextAsset textData = Resources.Load<TextAsset>("_World" + worldNumber + "/" + fileName);
        dialogueData = JsonMapper.ToObject(textData.ToString());
        
		DisplayDialogue (speaker, id);
		
		//Hide health display
		healthDisplay = GameObject.Find( "HealthDisplay");
		if( healthDisplay != null)
		{
			GameObject.Find("HealthDisplay").SetActive(false);
		}
	}

	void Pause()
	{
		if( playerStats )
			playerStats.paused = true;
	}

	void UnPause()
	{
		if( playerStats )
		playerStats.paused = false;
	}

	IEnumerator PrintText ()
	{
		//Store text characters in an array
		char[] characters = GetText(DisplaySpeaker, DisplayID).ToCharArray();
		//Empty text box
		DialogueTextUI.GetComponent<Text>().text = null;
		//Have I encountered a fomatting tag yet?
		bool tagEncountered = false;
		
		
		//Cycle through text and transfer it to text box
		for( int counter = 0; counter < characters.Length; counter++)
		{
			if(characters[counter] == '<' || characters[counter] == '>')
			{
				tagEncountered = !tagEncountered;
				continue;
			}
			else if(!tagEncountered){
				DialogueTextUI.GetComponent<Text>().text += characters[counter];
				
				GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("TextProgression");
			}
			else
			{
				continue;
			}
				yield return null;
		}
		DialogueTextUI.GetComponent<Text>().text = GetText(DisplaySpeaker, DisplayID).ToString();
	}
}
