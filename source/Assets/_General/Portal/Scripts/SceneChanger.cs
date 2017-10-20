using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    public Image black;

    public string currentIndex { get; private set; }

	void OnEnable()
	{
		//Calls a fade effect upon scene creation.
		Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 0.5f, 1, true);
	}
    void OnTriggerEnter2D(Collider2D coll) {
        //Initiate.Fade("", Color.black, 0.5f);

        if (coll.gameObject.name == "Player Physics Parent") {
			Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 0.8f, 0, false);
			
			Invoke("ChangeScene", 0.75f);
			
        }
    }
	void ChangeScene()
	{
		//Find current scene index
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
    
		//Change to next scene in build
		SceneManager.LoadSceneAsync(currentIndex + 1);
	}
}
