using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static bool load = false;

    //File stored in "C:\Users\###\AppData\LocalLow\Hybern\SWAP" as "checkpoint.extremelyimportantsavefile".
    //If you want to understand anything about this file ask Cian.

    //Save the Game
    //Called by checkpoint automatically
    public static void Save (int cpindex)
    {
        load = false;
		
		//Which scene is this?
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		/*
        Game Current = new Game(sceneIndex, cpindex);
        
		BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile"))
        {
            File.Delete(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile");
        bf.Serialize(file, Current);
        file.Close();
        */
        PlayerPrefs.SetInt("CPIndex", cpindex);
        PlayerPrefs.SetInt("SceneIndex", sceneIndex);
		WorldControl control;
        control = GameObject.Find("WorldControl").GetComponent<WorldControl>();
        control.StoreIndex( cpindex );
		control.StoreScene( sceneIndex );
    }

    //Load the Game
    public static void Load ()
    {
		GameObject error = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        int sceneIndex = PlayerPrefs.GetInt("SceneIndex", -1);
        int CPIndex = PlayerPrefs.GetInt("CPIndex", -1);
		if (sceneIndex > -1)
        {
			error.SetActive(false);
			load = true;
            /*
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile", FileMode.Open);
            Game Current = (Game)bf.Deserialize(file);
            file.Close();
            */
            //SceneManager.LoadSceneAsync(Current.SceneIndex);
            WorldControl control;
            control = GameObject.Find("WorldControl").GetComponent<WorldControl>();
			
			control.StoreIndex( CPIndex );
			control.StoreScene( sceneIndex );
			control.SwitchScene( sceneIndex );
        }
		else
		{
			error.SetActive(true);
		}
	}
	//Remove save file
	public static void DeleteSave ()
	{
        PlayerPrefs.SetInt("CPIndex", -1);
        PlayerPrefs.SetInt("SceneIndex", -1);
	}
}