using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour
{
    private int CPIndex;

    //Scene Control
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

    public void LoadButton()
    {
        SaveLoad.Load();
    }

    public void Exit()
    {
        Application.Quit();
    }

    //Loading Checkpoint Positioning
    public void OnEnable()
    {
        if (SaveLoad.load == true)
            SceneManager.activeSceneChanged += OnLevelFinishedLoading;
    }

    public void OnDisable()
    {
        if (SaveLoad.load == true)
            SceneManager.activeSceneChanged -= OnLevelFinishedLoading;
    }

    public void OnLevelFinishedLoading(Scene scene1, Scene scene2)
    {
        GameObject.Find("Player Physics Parent").transform.position = CheckPointPositions.CheckPointsVector3(CPIndex);
    }
    
    public void StoreIndex(int index)
    {
        CPIndex = index;
    }
}