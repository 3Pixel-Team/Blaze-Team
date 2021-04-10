using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	//Audio manager should be in a empty game object in the scene and should be filled with all the songs and sound effects you want

	public static AudioManager Instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("[Audio Manager] There is more than one Audio Manager!");
			Destroy(gameObject);
			return;
		}

		Instance = this;

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	//You can use method below to play a song / sound effect (calling it like AudioManager.Instance.Play("Sound effect"); for example)
	//The parameter should be the sound name that is in the Audio manager in the hierarchy
	public void Play(string sound) 
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	
	//If you wish a sound stops playing you should use this one, also calling the name of the sound
	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		
		s.source.Stop();
	}

	public bool IsPlayingSound(string soundName)
    {
		Sound s = Array.Find(sounds, item => item.name == soundName);
		if (s == null)
		{
			return true;
		}

		return s.source.isPlaying;
	}
}
