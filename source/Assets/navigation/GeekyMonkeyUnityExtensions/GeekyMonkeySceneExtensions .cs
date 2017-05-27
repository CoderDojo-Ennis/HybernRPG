using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GeekyMonkeySceneExtensions
{
    /// <summary>
    /// Get or Create an empty game object to serve as a folder for other game objects
    /// </summary>
    /// <param name="folderName">The folder name to find or create</param>
    /// <returns>Folder game object</returns>
    public static GameObject Folder(this Scene scene, string folderName)
    {
        var rootObjects = scene.GetRootGameObjects();
        GameObject folder = null;
        foreach (var rootObject in rootObjects)
        {
            if (rootObject.name == folderName)
            {
                folder = rootObject;
                break;
            }
        }

        if (folder == null)
        {
            folder = new GameObject(folderName);
        }

        return folder;
    }
}
