using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public AudioClip footFall1;
	public AudioClip footFall2;
	
	void PlaySoundOfFootstep1()
	{
		GetComponent<AudioSource>().PlayOneShot(footFall1, 1.0f);
	}
	void PlaySoundOfFootstep2()
	{
		GetComponent<AudioSource>().PlayOneShot(footFall2, 1.0f);
	}
}
