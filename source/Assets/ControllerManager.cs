using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
	public static ControllerManager instance;
	public bool ControllerConnected = false;
	public Vector3 SpoofedMousePosition;
	private GameObject Player;
	void Awake ()
	{
		Player = GameObject.Find("Player Physics Parent");
		if (instance != null)
		{
			Destroy(instance);
		}
		instance = this;
	}
	
	void Update ()
	{
		if (CheckIfControllerConnected())
		{
			if (ControllerConnected == false)
			{
				ControllerConnected = true;
				ControllerJustConnected();
			}
			SpoofedMousePosition = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(0, 0.5f) + new Vector3(Input.GetAxis("Right Stick X"), Input.GetAxis("Right Stick Y") * -1) * 10);
			//transform.position = Camera.main.ScreenToWorldPoint(SpoofedMousePosition);
		}
		else
		{
			SpoofedMousePosition = Input.mousePosition;
			if (ControllerConnected == true)
			{
				ControllerConnected = false;
				ControllerJustDisconnected();
			}
		}
	}

	bool CheckIfControllerConnected()  //Man, Unity's controller API sucks
	{
		if (Input.GetJoystickNames().Length > 0)
		{
			foreach (string j in Input.GetJoystickNames())
			{
				if (!string.IsNullOrEmpty(j))
				{
					return true;
				}
			}
			return false;
		}
		else
		{
			return false;
		}
	}

	void ControllerJustConnected()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void ControllerJustDisconnected()
	{
		Cursor.lockState = CursorLockMode.None;
	}
}
