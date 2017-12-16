﻿using UnityEngine;

public class Execute : MonoBehaviour 
{
    public WorldControl worldControl;
	public GameObject ammo;
    public JSONDialogueReader reader;
	public bool fired = false;
	
	void Start ()
	{
        worldControl = GameObject.Find("WorldControl").GetComponent<WorldControl>();
        reader = GameObject.Find("TextBoxCanvas").GetComponent<JSONDialogueReader>();
	}
	
	void Update()
	{
		if (Input.GetMouseButtonUp(0) && !fired && reader.shootConnor == true)
		{
            fired = true;
            Murder();
        }
	}

    void Murder()
    {
        Instantiate(ammo, this.transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.AngleAxis(90, Vector3.forward));
    }
}
