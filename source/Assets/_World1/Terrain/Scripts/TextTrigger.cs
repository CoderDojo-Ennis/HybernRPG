using UnityEngine;

public class TextTrigger : MonoBehaviour {

	private JSONDialogueReader dialogue;
	public int worldNumber;
	public string fileName;
	public string speaker;
	public string id;
	
	void OnEnable()
	{
		dialogue = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
	}
	public void OnTriggerEnter2D()
	{
		dialogue.BeginDialogue (worldNumber, fileName, speaker, id);
        Destroy(this.GetComponent<TextTrigger>());
	}
}
