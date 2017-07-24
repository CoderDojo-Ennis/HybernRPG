using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

	public AudioClip music;
	
	void OnTriggerEnter2D()
	{
		GameObject camera;
		camera = GameObject.Find("Main Camera");
	
		AudioSource source;
		source =camera.GetComponent<AudioSource>();
		
		source.clip = music;
		source.Play();
		
		GameObject.Destroy( this.gameObject );
	}
}
