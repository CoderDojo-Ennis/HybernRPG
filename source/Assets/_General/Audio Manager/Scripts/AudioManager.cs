using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioMixerGroup audioMixer;
    public Sound[] sounds;

	// Use this for initialization
	void Awake () {
        foreach (Sound s in sounds)
            {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
			if(s.useAudioMixer)
			s.source.outputAudioMixerGroup = audioMixer;
			s.source.playOnAwake = false;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
		
	}

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.source.clip, s.source.volume);
    }
	public void Stop (string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		
		s.source.enabled = false;
		s.source.enabled = true;
	}
	
}
