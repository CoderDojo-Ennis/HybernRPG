using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour
{
    private static int CPIndex = -1;
	private static int sceneIndex = -1;
	private int sceneToSwitchTo;

    //Scene Control
    public void SwitchScene(int i)
    {
		//Fade out of scene
		Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 1f, 0, false);
		
		//Tell computer which scene we want to switch to.
		sceneToSwitchTo = i;
		
		Invoke("InitiateSwithcSceneLoading", 1.2f);
		
    }
	public void NextScene ()
	{
		//Fade out of scene
		Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 0.9f, 0, false);
		
		//Call code for loading next scene after a delay.
		//The delay is necessary in order to allow the screen time to fade to black.
        Invoke("InitiateNextSceneloading", 0.9f);
	}

    public void ReloadScene()
	{
		//Fade out of scene.
		Initiate.Fade(SceneManager.GetActiveScene().ToString(), Color.black, 0.8f, 0, false);
			
		//Call code for reloading scene after a delay.
		//The delay is necessary in order to allow the screen time to fade to black.
		Invoke("InitiateSceneReloading", 1.2f);
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
	//Actual code for reloading scene.
	//called from SceneReload after a delay
	//to insure fade effect can occur first.
	public void InitiateSceneReloading()
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
	public void InitiateNextSceneloading()
	{
		//Find current scene index
		int currentIndex = SceneManager.GetActiveScene().buildIndex;
		
		//load next scene
		SceneManager.LoadSceneAsync(currentIndex + 1);
	}
	public void InitiateSwithcSceneLoading()
	{
		//using stored value, switch to a new scene;
        SceneManager.LoadSceneAsync(sceneToSwitchTo);
	}
}