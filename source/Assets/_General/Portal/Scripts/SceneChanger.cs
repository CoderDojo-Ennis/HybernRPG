using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    public Image black;

    public string currentIndex { get; private set; }

    void OnTriggerEnter2D(Collider2D coll) {
        Initiate.Fade("", Color.black, 0.5f);
        Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 0.5f);

        if (coll.gameObject.name == "Player Physics Parent") {
			//Find current scene index
			int currentIndex = SceneManager.GetActiveScene().buildIndex;
            
			//Change to next scene in build
			SceneManager.LoadScene(currentIndex + 1);
        }
    }
}
