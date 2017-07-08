using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour {

    public void SwitchScene(int i)
    {
        SceneManager.LoadSceneAsync(i);
    }
	public void ReloadScene()
	{
		//Find current scene index
		int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //Change to next scene in build
        SceneManager.LoadSceneAsync(currentIndex);
	}

    public void Exit()
    {
        Application.Quit();
    }
}