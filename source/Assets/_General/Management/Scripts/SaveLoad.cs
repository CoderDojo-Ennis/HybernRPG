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
		
        Game Current = new Game(sceneIndex, cpindex);
        
		BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile"))
        {
            File.Delete(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile");
        bf.Serialize(file, Current);
        file.Close();
		WorldControl control;
        control = GameObject.Find("WorldControl").GetComponent<WorldControl>();
        control.StoreIndex( Current.CPIndex );
		control.StoreScene( Current.SceneIndex );
    }

    //Load the Game
    public static void Load ()
    {
        if (File.Exists(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile"))
        {
            load = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile", FileMode.Open);
            Game Current = (Game)bf.Deserialize(file);
            file.Close();
            //SceneManager.LoadSceneAsync(Current.SceneIndex);
            WorldControl control;
            control = GameObject.Find("WorldControl").GetComponent<WorldControl>();
			
			control.StoreIndex( Current.CPIndex );
			control.StoreScene( Current.SceneIndex );
			
			control.SwitchScene( Current.SceneIndex );
        }
    }
	//Remove save file
	public static void DeleteSave ()
	{
		if (File.Exists(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile"))
        {
            File.Delete(Application.persistentDataPath + "/checkpoint.extremelyimportantsavefile");
        }
	}
}