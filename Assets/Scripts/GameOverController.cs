using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public Text m_scoreText;
	public Text m_bestScoreText;

	private ScoreDataController m_scoreDataController;


	void Start()
	{
		GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().PlayMusic();

		m_scoreDataController = FindObjectOfType<ScoreDataController>();

		m_scoreText.text = "Score: " + PlayerController.m_playerScore;
		m_bestScoreText.text = "Best: " + m_scoreDataController.GetBestScore ();
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene ("MenuScreen");
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene ("Level 1");
	}
}
