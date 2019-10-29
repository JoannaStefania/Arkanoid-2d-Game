using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Boundary
{
	public float xMin, xMax;
}

[System.Serializable]
public struct TweenSettings
{
	public float tweenDuration;
	public float growth;
}

[System.Serializable]
public struct ScoreSettings
{
	public int pointsForBlock;
	public int pointsForBonus;
}

public class PlayerController : MonoBehaviour {

	public float m_speed=8f;

	public Boundary m_boundary;
	public TweenSettings m_tweenSettings;
	public ScoreSettings m_scoreSettings;

	public static int m_playerScore;


	private Vector2 m_playerStartPosition;
	private Vector3 m_playerStartScale;
	private Vector3 m_playerCurrentScale;

	private Rigidbody2D m_rb2d;

	private bool m_tweenActive;

	private float m_currentTweenDuration;
	private float m_changeNeeded;



	void Start()
	{
		m_playerStartPosition = DataController.LoadLvlData ().playerStartPosition;

		m_rb2d = GetComponent<Rigidbody2D> ();
			
		m_rb2d.position = new Vector2 (m_playerStartPosition.x, m_playerStartPosition.y);

		m_playerStartScale = transform.localScale;
		m_playerCurrentScale = m_playerStartScale;

		m_tweenActive = false;

		if (gameObject.scene.name == "Level 1") {
			m_playerScore = 0;
		}

	}
		
	void FixedUpdate()
	{
		UpdatePlayerMovement ();
	}

	void Update()
	{
		UpdateVausSize ();
	}
		
	void UpdatePlayerMovement()
	{
		float l_boundaryXLeft = m_boundary.xMin + ((m_playerCurrentScale.x - m_playerStartScale.x) / 2);
		float l_boundaryXRight = m_boundary.xMax - ((m_playerCurrentScale.x - m_playerStartScale.x) / 2);

		float l_moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 l_movement = new Vector2 (l_moveHorizontal, 0.0f );

		m_rb2d.velocity = l_movement * m_speed;

		m_rb2d.position = new Vector2 (Mathf.Clamp(m_rb2d.position.x, l_boundaryXLeft, l_boundaryXRight), m_playerStartPosition.y);
	}

	void UpdateVausSize()
	{
		if (m_tweenActive) 
		{
			if (m_currentTweenDuration >= m_tweenSettings.tweenDuration) 
			{
				m_tweenActive = false;

				m_playerCurrentScale = transform.localScale;

			} 
			else 
			{
				m_currentTweenDuration += Time.deltaTime;

				SetVausSize (m_changeNeeded, m_tweenSettings.tweenDuration);
			}
		} 
	}


	void SetVausSize(float _changeNeeded, float _easingDuration)
	{
		float l_tweenMove = Tweens.EaseOutElastic (m_currentTweenDuration, m_playerCurrentScale.x, _changeNeeded, _easingDuration,1.5f);
		transform.localScale = new Vector3 (l_tweenMove, transform.localScale.y);
	}

	public void TweenActivator(string _sizeDirection)
	{
		m_tweenActive = true;
		m_currentTweenDuration = 0;

		if (_sizeDirection == "grow") 
		{
			m_changeNeeded = m_tweenSettings.growth;
		} 
		else if (_sizeDirection == "reset") 
		{
			m_playerCurrentScale = transform.localScale;
			m_changeNeeded = m_playerStartScale.x - m_playerCurrentScale.x;

			if (m_changeNeeded >= -0.05f) 
			{
				m_tweenActive = false;
			} 
		}
	}

	public int GetPlayerScore()
	{
		return m_playerScore;
	}

	public int GetPoints(string _poinType)
	{
		if (_poinType == "bonus") 
		{
			return m_scoreSettings.pointsForBonus;
		} 

		else if (_poinType == "block") 
		{
			return m_scoreSettings.pointsForBlock;
		} 

		else 
		{
			Debug.LogError ("Not such point type! ");
			return 0;
		}

	}

	public float GetPlayerGrowth()
	{
		return m_tweenSettings.growth;
	}

}
