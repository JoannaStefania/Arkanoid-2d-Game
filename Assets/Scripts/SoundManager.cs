using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static AudioClip m_BlockHitSound, m_VausHitSound, m_WallHitSound, m_gameSong;

	private static AudioSource m_audioSrc;

	void Start () 
	{
		m_BlockHitSound = Resources.Load<AudioClip> ("BlockHitShort");
		m_VausHitSound = Resources.Load<AudioClip> ("VausHitShort");
		m_WallHitSound = Resources.Load<AudioClip> ("WallHitShort");

		m_audioSrc = GetComponent<AudioSource> ();
	}

	public static void PlaySound(string clip)
	{
		switch (clip) 
		{
		case "BlockHitShort":
			m_audioSrc.PlayOneShot (m_BlockHitSound);
			break;
		case "VausHitShort":
			m_audioSrc.PlayOneShot (m_VausHitSound);
			break;
		case "WallHitShort":
			m_audioSrc.PlayOneShot (m_WallHitSound);
			break;
		}
	}

	public static void PlayGameMusic()
	{
		if (m_audioSrc.isPlaying) 
		{
			return;
		}

		m_audioSrc.Play();
	}
}
