using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class ChangeLimb : MonoBehaviour {
    AnimationControl animationControl;
    private GameObject child;
	private GameObject CurrentHovered;

    public movement Movement;
    public GameObject wheel;
	public GameObject healthDisplay;
	public PlayerStats playerStats;
    
    /// is significantly incomplete
    
	//Arm Limbs
	//0 - normal
	///1 - pickaxes
	//2 - shield
	//3 - grappling hook
	//7 - arm cannon
	
	//Torso Limbs
	//0 - normal
	//1 - heavy torso
    //2 - jetpack
	//3 - cactus
	
	void Start ()
	{
		child = transform.GetChild(0).gameObject;
        animationControl = child.GetComponent<AnimationControl>();
        wheel = GameObject.Find("UI").transform.GetChild(0).gameObject;
		healthDisplay = GameObject.Find("UI").transform.GetChild(1).gameObject;
		playerStats = GetComponent< PlayerStats >();
    }

    void Update ()
	{
		if(!playerStats.paused)
		{
			/*
			//Hotkeys disabled for time being
			//Arms
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SwitchArms(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				SwitchArms(7);
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				SwitchArms(3);
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				SwitchArms(2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				SwitchArms(1);
			}

			//Torso
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				SwitchTorso(3);
			}
			if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				SwitchTorso(2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha9))
			{
				SwitchTorso(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				SwitchTorso(1);
			}*/
			
			//Part Wheel
			if (Input.GetButtonDown("OpenPartWheel"))
			{
				WheelControl(true);
			}
			if (Input.GetButtonUp("OpenPartWheel"))
			{
				WheelControl(false);
			}
			var pointer = new PointerEventData(EventSystem.current);
			if (wheel.activeSelf && ControllerManager.instance.ControllerConnected)
			{
				Transform closest = transform;
				float closestDistance = 1000000;
				foreach(Transform t in wheel.transform.Find("Background"))
				{
					if (t.gameObject.activeInHierarchy)
					{
						ControllerManager.instance.transform.position = Camera.main.ScreenToWorldPoint(t.position);
						float dist = Vector3.Distance(Camera.main.ScreenToWorldPoint(t.position), Camera.main.ScreenToWorldPoint(ControllerManager.instance.SpoofedMousePosition));
						if (dist < closestDistance)
						{
							closestDistance = dist;
							closest = t;
						}
					}
				}
				if (closest.gameObject != CurrentHovered)
				{
					if (CurrentHovered != null)
					{
						ExecuteEvents.Execute(CurrentHovered, pointer, ExecuteEvents.pointerExitHandler);
					}
					CurrentHovered = closest.gameObject;
					ExecuteEvents.Execute(CurrentHovered, pointer, ExecuteEvents.pointerEnterHandler);
				}
			}
		}
    }

    public void SwitchArms(int i)
    {
        animationControl.ArmLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs, animationControl.HeadLimbs);
    }

    public void SwitchTorso(int i)
    {
        animationControl.TorsoLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, i, animationControl.HeadLimbs);
    }

    public void SwitchHead(int i)
    {
        animationControl.HeadLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs, animationControl.HeadLimbs);
    }

	public void WheelControl(bool open)
	{
		//wheel.SetActive(!wheel.activeSelf);
		//healthDisplay.SetActive(!healthDisplay.activeSelf);
		//Movement.enabled = !Movement.enabled;
		wheel.SetActive(open);
		healthDisplay.SetActive(!open);
		Movement.enabled = !open;
		//Time Control
		if (open)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
			Text text = wheel.transform.Find("Text Panel").GetComponentInChildren<Text>();
			switch (text.text) //Kindof a hack, but whatever
			{
				case "Normal Arms":
					SwitchArms(0);
					break;
				case "Pickaxe":
					SwitchArms(1);
					break;
				case "Shield":
					SwitchArms(2);
					break;
				case "Grappling Hook":
					SwitchArms(3);
					break;
				case "Arm Cannon":
					SwitchArms(7);
					break;
				case "Normal Torso":
					SwitchTorso(0);
					break;
				case "Heavy Torso":
					SwitchTorso(1);
					break;
				case "Jetpack":
					SwitchTorso(2);
					break;
				case "Cactus":
					SwitchTorso(3);
					break;
			}
		}
    }
}
