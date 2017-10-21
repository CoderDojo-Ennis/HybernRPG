using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    public Image black;

    public string currentIndex { get; private set; }

    void OnTriggerEnter2D(Collider2D coll) {
	
        if (coll.gameObject.name == "Player Physics Parent") {
			//Fade out of scene.
			Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 0.8f, 0, false);
			
			//Change scene after a delay.
			Invoke("ChangeScene", 0.9f);
			
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
