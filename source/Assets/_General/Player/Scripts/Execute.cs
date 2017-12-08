using UnityEngine;

public class Execute : MonoBehaviour 
{
    public WorldControl worldControl;
	public GameObject ammo;
	public bool fired = false;
	
	void Start ()
	{
        worldControl = GameObject.Find("WorldControl").GetComponent<WorldControl>();
	}
	
	void Update()
	{
		if (Input.GetMouseButtonUp(0) && !fired)
		{
            worldControl.NextScene();
            fired = true;
            Invoke("Murder", 0.7f);
        }
	}

    void Murder()
    {
        Instantiate(ammo, this.transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.AngleAxis(90, Vector3.forward));
    }
}
