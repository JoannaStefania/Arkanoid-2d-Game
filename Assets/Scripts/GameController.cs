using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text m_lifeText;
	public Text m_midText;
	public Text m_scoreText;
	public Text m_levelInfoText;

	public GameObject m_player;


	private ScoreDataController m_scoreDataController;

	private GameObject m_mainBall;
	private GameObject m_ballClone;

	private bool m_endGame;



	void Start()
	{
		m_mainBall = GameObject.FindWithTag ("Ball");
		m_ballClone = GameObject.FindWithTag ("BallClone");

		m_scoreDataController = FindObjectOfType<ScoreDataController>();

		GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().StopMusic();
		SoundManager.PlayGameMusic ();

		Destructible.ResetDestroyedBlocks ();

		m_endGame = false;

		SetLifeText ();
		SetPlayerScoreText ();
		m_midText.text = "";
		m_levelInfoText.text = gameObject.scene.name;
	}

	void Update()
	{
		GameObject l_blocks = GameObject.FindGameObjectWithTag("Block");

		if (l_blocks == null )
		{
			switch (gameObject.scene.name) 
			{
				case "Level 2":
					GameWin ();
					break;
				case "Level 1":
					EndLvl ();
					break;
			}
		} 

		else if (BallsLeft () <= 0 && m_endGame == true && m_ballClone == null)
		{
			EndGame ();
		} 

		SetLifeText ();
		SetPlayerScoreText ();
	}

	void SetLifeText()
	{
		m_lifeText.text = "Balls: " + BallsLeft();
	}

	void SetPlayerScoreText()
	{
		m_scoreText.text = "Score: " + PlayerScore ();
	}

	int BallsLeft()
	{
		int l_ballsLeft = m_mainBall.GetComponent<BallController> ().GetBallsAmount ();
		return l_ballsLeft;
	}

	int PlayerScore()
	{
		int l_player = m_player.GetComponent<PlayerController> ().GetPlayerScore ();
		return l_player;
	}

	void EndGame()
	{
		m_scoreDataController.SubmitNewScore (PlayerScore ());

		SceneManager.LoadScene ("GameOver");
	}

	public bool SetEndGame()
	{
		m_endGame = true;
		return m_endGame;
	}

	void GameWin()
	{
		m_scoreDataController.SubmitNewScore (PlayerScore ());

		m_midText.text = "You win !";

		StartCoroutine(LoadSceneAfterDelay(2.5f,"MenuScreen"));
	}

	void EndLvl()
	{
		m_midText.text = "Next level !";
		StartCoroutine(LoadSceneAfterDelay(1.5f, "Level 2"));
	}

	IEnumerator LoadSceneAfterDelay(float _delay, string _sceneName)
	{
		yield return new WaitForSeconds (_delay);

		SceneManager.LoadScene (_sceneName);
	}

}
