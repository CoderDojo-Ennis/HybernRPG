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
<<<<<<< HEAD
            
			//Change to next scene in build
			SceneManager.LoadScene(currentIndex + 1);
=======
            
			//Change to next scene in build
			Application.LoadLevel(currentIndex + 1);
           
			//If this is a portal
			GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PortalSoundEffect");
>>>>>>> 71be30888df63a4222f7ef97ca82aa22d96ba444
        }
    }
}
