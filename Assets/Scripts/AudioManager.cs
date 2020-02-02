using UnityEngine.Audio;
using System;
using UnityEngine;

//Audio Manager from Brackeys
//(Edits + Comments from Harper)

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		//Manager Persists across scenes

		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			//DontDestroyOnLoad(gameObject);
		}

		//Add an audio source to the manager for each sound 

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	void Start(){
		Play("Theme1");
	}


	//Plays a sound given its name (silent error if no such sound exists)
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		//s.source.volume = s.volume; //* (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch; //* (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

}
