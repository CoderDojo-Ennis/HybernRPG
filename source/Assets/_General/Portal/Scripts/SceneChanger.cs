using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll) {

        if(coll.gameObject.name == "Player Physics Parent") {
			//Find current scene index
			int currentIndex = SceneManager.GetActiveScene().buildIndex;
            
			//Change to next scene in build
			Application.LoadLevel(currentIndex + 1);
           
			//If this is a portal
			GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("PortalSoundEffect");
        }
    }
}
