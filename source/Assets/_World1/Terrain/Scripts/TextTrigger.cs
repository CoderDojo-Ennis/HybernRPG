using UnityEngine;

public class TextTrigger : MonoBehaviour {

	private JSONDialogueReader dialouge;
	public int worldNumber;
	public string fileName;
	public string speaker;
	public string id;
	
	void OnEnable()
	{
		dialouge = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
	}
	public void OnTriggerEnter2D()
	{
		dialouge.BeginDialogue (worldNumber, fileName, speaker, id);
        Destroy(this.GetComponent<TextTrigger>());
	}
}
