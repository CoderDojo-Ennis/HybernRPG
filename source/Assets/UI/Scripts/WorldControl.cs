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

    public void Exit()
    {
        Application.Quit();
    }
}