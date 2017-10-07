using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour
{
    private static int CPIndex = -1;
	private static int sceneIndex = -1;

    //Scene Control
    public void SwitchScene(int i)
    {
		//Find current scene index
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		
        SceneManager.LoadSceneAsync(i);
    }
	public void NextScene ()
	{
		//Find current scene index
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		
        SceneManager.LoadSceneAsync(currentIndex + 1);
	}

    public void ReloadScene()
	{
		//Find current scene index
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		
		if(CPIndex != -1)
		{
			//Position player at any active checkpoints
			SaveLoad.load = true;
		}

        //Change to next scene in build
        SceneManager.LoadSceneAsync(currentIndex);
	}
	public void NewGame ()
	{
		SaveLoad.DeleteSave ();
		SwitchScene(1);
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
    	SceneManager.activeSceneChanged += OnLevelFinishedLoading;
    }

    public void OnDisable()
    {
		SceneManager.activeSceneChanged -= OnLevelFinishedLoading;
    }

    public void OnLevelFinishedLoading(Scene scene1, Scene scene2)
    {
		Time.timeScale = 1;
		
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		
		if ( currentIndex == sceneIndex )
		{
			GameObject.Find("Player Physics Parent").transform.position = GameObject.Find("" + CPIndex).transform.position;
			//Debug.Log("current index: " + currentIndex);
			//Debug.Log("current sceneIndex: " + sceneIndex);
			//Debug.Log("current CPIndex: " + CPIndex);
		}
    }
    
    public void StoreIndex(int index)
    {
        CPIndex = index;
    }
	public void StoreScene(int index)
    {
        sceneIndex = index;
    }
}