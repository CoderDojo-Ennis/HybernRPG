using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    public Image black;
    public Animator anim;

    public string currentIndex { get; private set; }

    void OnTriggerEnter2D(Collider2D coll) {        
        StartCoroutine(Fading());   

        if(coll.gameObject.name == "Player Physics Parent") {
			//Find current scene index
			int currentIndex = SceneManager.GetActiveScene().buildIndex;
            
			//Change to next scene in build
			Application.LoadLevel(currentIndex + 1);
            
        }
    }
    IEnumerator  Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(currentIndex + 1);
    }
}
