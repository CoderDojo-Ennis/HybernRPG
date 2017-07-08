using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLimb : MonoBehaviour {
    AnimationControl animationControl;
    private bool time;
    private GameObject child;

    public movement Movement;
    public GameObject wheel;
    
    /// is significantly incomplete
    
	//Arm Limbs
	//0 - normal
	///1 - pickaxes
	//2 - shield
	//3 - grapplin hook
    ///4 - wings
	//7 - arm cannon
	
	//Torso Limbs
	//0 - normal
	//1 - heavy torso
    ///2 - cactus || jetpack
	
	void Start ()
	{
		child = transform.GetChild(0).gameObject;
        animationControl = child.GetComponent<AnimationControl>();
        wheel = GameObject.Find("UI").transform.GetChild(0).gameObject;
    }

    void Awake()
    {
        time = true;
    }

    void Update ()
	{
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchArms(4);
        }

        //Torso
        if (Input.GetKeyDown(KeyCode.Alpha9))
		{
            SwitchTorso(0);
        }
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
            SwitchTorso(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchTorso(2);
        }

        //Part Wheel
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            WheelControl();
		}

        //Time Control
        if (time)
        {
            Time.timeScale = 1;
        }
        if (time == false)
        {
            Time.timeScale = 0.1f;
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
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs, animationControl.HeadLimbs);
    }

    public void SwitchHead(int i)
    {
        animationControl.HeadLimbs = i;
        child.GetComponent<SpriteControl>().SetSprites(animationControl.ArmLimbs, animationControl.TorsoLimbs, animationControl.HeadLimbs);
    }

    public void WheelControl()
    {
        wheel.SetActive(!wheel.activeSelf);
        time = !time;
        Movement.enabled = !Movement.enabled;
    }
}
