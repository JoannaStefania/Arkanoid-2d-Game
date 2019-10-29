using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour {

	public Text m_bestScoreText;

	private ScoreDataController m_scoreDataController;



	void Start()
	{
		GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().PlayMusic();

		m_scoreDataController = FindObjectOfType<ScoreDataController> ();

		m_bestScoreText.text = "Best: " + +m_scoreDataController.GetBestScore ();
	}


	public void StartGame()
	{
		SceneManager.LoadScene ("Level 1");
	}

	public void Exit()
	{
		Debug.Log ("Application has quit");
		Application.Quit ();
	}
}
