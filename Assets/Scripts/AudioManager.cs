using UnityEngine.Audio;
using System;
using UnityEngine;

//Audio Manager from Brackeys
//(Edits + Comments from Harper)

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;
	public AudioMixer mixer;
	public float initialSliderVal=0.948f; //initial slider value not updating vol. automatically yet
	private bool bossFightThemePlaying=false;

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
			DontDestroyOnLoad(gameObject);
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
		SetLevel(initialSliderVal);
		Play("mainTheme");
	}

	void Update(){

		//switch songs during boss fight
		if(GameManager.bossFightInProgress && !bossFightThemePlaying){
			Stop("mainTheme");
			Play("bossTheme");
			bossFightThemePlaying=true;
		} else if(!GameManager.bossFightInProgress && bossFightThemePlaying){
			Stop("bossTheme");
			Play("mainTheme");
			bossFightThemePlaying=false;
		}
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

	//Plays a sound given its name (silent error if no such sound exists)
	public void PlayOneShot(string sound, float volume)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		//s.source.volume = s.volume; //* (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch; //* (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.PlayOneShot(s.clip,volume);
	}

		//Plays a sound given its name (silent error if no such sound exists)
	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		//s.source.volume = s.volume; //* (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		//s.source.pitch = s.pitch; //* (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}

	public void SetLevel(float sliderValue){
		mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue)*20);
	}

}
