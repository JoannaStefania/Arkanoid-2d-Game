using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDataController : MonoBehaviour {

	private BestScore m_bestScore;



	void Start () 
	{
		DontDestroyOnLoad (gameObject);
		LoadBestScore ();

		SceneManager.LoadScene ("MenuScreen");
	}


	void LoadBestScore()
	{
		m_bestScore = new BestScore ();

		if (PlayerPrefs.HasKey ("highestScore")) 
		{
			m_bestScore.m_highestScore = PlayerPrefs.GetInt ("highestScore",0);
		}
	}

	void SaveBestScore()
	{
		PlayerPrefs.SetInt ("highestScore", m_bestScore.m_highestScore);
	}

	public void SubmitNewScore(int _newScore)
	{
		if (_newScore > m_bestScore.m_highestScore) 
		{
			m_bestScore.m_highestScore = _newScore;

			SaveBestScore ();
		}
	}

	public int GetBestScore()
	{
		return m_bestScore.m_highestScore;
	}
}
