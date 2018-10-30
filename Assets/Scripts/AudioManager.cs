using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;



	// Use this for initialization
	void Awake () {



		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
		}

	}

	void Start()
	{
		Play ("Theme");
	}
	public void Play (string name)
	{
		Sound s = Array.Find(sounds, item => item.name == name);
		if (s == null) 
		{
			Debug.LogWarning ("Sound: " + name + " not found!!");
			return;
		}
		s.source.Play ();

	}
}

