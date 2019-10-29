using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	private AudioSource m_audioSrc;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);

		m_audioSrc = GetComponent<AudioSource> ();
	}

	public void PlayMusic()
	{
		if (m_audioSrc.isPlaying) 
		{
			return;
		}

		m_audioSrc.Play();
	}

	public void StopMusic()
	{
		m_audioSrc.Stop ();
	}
}
