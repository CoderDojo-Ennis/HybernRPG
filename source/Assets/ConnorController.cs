using UnityEngine.SceneManagement;
using UnityEngine;

public class ConnorController : MonoBehaviour 
{
	public WorldControl worldControl;
	public GameObject black;
	public void Win()
	{
		worldControl = GameObject.Find("WorldControl").GetComponent<WorldControl>();
        black = GameObject.Find("BlackScreen");
		black.transform.position = new Vector3(0, 0, 0);
		worldControl.NextScene();
	}
}
