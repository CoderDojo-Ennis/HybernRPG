using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

	private GameObject currentButton;
	private AxisEventData currentAxis;
	//timer
	private float timeBetweenInputs = 0.15f; //in seconds
	private float timer = 0;

	void Update ()
	{
		if (CheckIfControllerConnected())
		{
			if (ControllerConnected == false)
			{
				ControllerConnected = true;
				ControllerJustConnected();
			}
			if (Player != null)
			{
				SpoofedMousePosition = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(0, 0.5f) + new Vector3(Input.GetAxis("Right Stick X"), Input.GetAxis("Right Stick Y") * -1) * 10);
			}
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

		if (timer == 0)
		{
			currentAxis = new AxisEventData(EventSystem.current);
			currentButton = EventSystem.current.currentSelectedGameObject;

			if (Input.GetAxis("Vertical") > 0) // move up
			{
				currentAxis.moveDir = MoveDirection.Up;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			}
			else if (Input.GetAxis("Vertical") < 0) // move down
			{
				currentAxis.moveDir = MoveDirection.Down;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			}
			else if (Input.GetAxis("Horizontal") > 0) // move right
			{
				currentAxis.moveDir = MoveDirection.Right;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			}
			else if (Input.GetAxis("Horizontal") < 0) // move left
			{
				currentAxis.moveDir = MoveDirection.Left;
				ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
				timer = timeBetweenInputs;
			}
		}

		//timer counting down
		if (timer > 0) { timer -= Time.deltaTime; } else { timer = 0; }
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
