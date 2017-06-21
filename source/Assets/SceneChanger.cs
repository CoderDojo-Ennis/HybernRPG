using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public string scene;

    void OnTriggerEnter2D(Collider2D coll) {

        if(coll.gameObject.name == "Player Physics Parent") {
            Application.LoadLevel(scene);
            //Here it would use the "level" variable to load the next scene
        }
    }
}
