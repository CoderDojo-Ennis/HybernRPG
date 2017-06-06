using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	Button myButton;

	void Awake()
	{
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener( () => {WorldOne();} );
	}

	public void WorldOne () 
	{
		SceneManager.LoadSceneAsync(1);
	}
}
