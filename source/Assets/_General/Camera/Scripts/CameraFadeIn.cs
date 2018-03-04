using UnityEngine;

public class CameraFadeIn : MonoBehaviour {

	void OnEnable()
	{
		//Calls a fade effect upon entering a scene.
		Initiate.Fade("", Color.black, 0.5f, 1, true);
	}
}
