using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	bool Paused = false;
	PlayerStats playerStats;
	public GameObject PausePanel;
	public Button SelectedButton;
	void Start ()
	{
		if (GameObject.Find("Player Physics Parent"))
		{
			playerStats = GameObject.Find("Player Physics Parent").GetComponent<PlayerStats>();
		}
	}

	void Update ()
	{
		if (Input.GetButtonDown("Pause"))
		{
			if (Paused)
			{
				CloseMenu();
			}
			else
			{
				playerStats.paused = true;
				Time.timeScale = 0;
				PausePanel.SetActive(true);
				SelectedButton.Select();
			}
			Paused = !Paused;
		}
	}

	public void CloseMenu()
	{
		playerStats.paused = false;
		Time.timeScale = 1;
		PausePanel.SetActive(false);
	}
}
