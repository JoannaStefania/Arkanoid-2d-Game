using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BounceTweenSetting
{
	public float tweenDuration;
	public float ballGrowth;
}

public class BallController : MonoBehaviour {

	public float m_speed=8;

	public BounceTweenSetting m_bounceTweenSetting;


	private Rigidbody2D m_rb2d;
	private Vector3 m_ballStartScale;

	private GameObject m_player;
	private GameController m_gameController;
	private GameObject m_camera;

	private string[] m_lastHitObj;
	private string m_objectName;

	private bool m_isShoot;
	private bool m_tweenIsActive;

	private float m_currentTweenDuration;
	private int m_ballsLeft;


	void Awake()
	{
		m_lastHitObj = new string[2];
	
		m_gameController = FindObjectOfType<GameController> ();
		m_player = GameObject.FindWithTag ("Player");
		m_camera = GameObject.FindWithTag("MainCamera");

		m_rb2d = GetComponent<Rigidbody2D> ();
		m_ballStartScale = transform.localScale;
		m_objectName = gameObject.name;

		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		m_tweenIsActive = false;
	}

	void Start()
	{
		m_ballsLeft = 3;

		if (m_objectName == "Ball") {
			m_isShoot = false;
		}
		else 
		{	gameObject.tag = "BallClone";}
	}

	void Update()
	{
		GameObject l_ballClone = GameObject.FindGameObjectWithTag ("BallClone");

		if (gameObject.name == "Ball") 
		{
			if (m_isShoot == false) 
			{
				InPositionBall ();

				if (Input.GetKey ("space")) 
				{ GoBall ();} 
			} 

			else
			{
				if(l_ballClone == null && transform.position.y < -4f) 
				{
					if (m_ballsLeft > 0) {
						m_player.GetComponent<PlayerController> ().TweenActivator ("reset");
						RespawnBall ();
					} 
					else 
					{
						m_gameController.GetComponent<GameController> ().SetEndGame ();
					}
				}
			}
	
			if(l_ballClone!=null)
			{
				Physics2D.IgnoreCollision (l_ballClone.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
			}
		}
		BounceTween ();
	}

	void FixedUpdate()
	{
		if (m_rb2d.velocity.magnitude < 0.6f * m_speed) 
		{
			m_rb2d.velocity = m_rb2d.velocity.normalized * m_speed;
		}

		if (Mathf.Abs (m_rb2d.velocity.y) < 1.5f) 
		{
			if (m_rb2d.velocity.y <= 0) 
			{m_rb2d.AddForce (transform.up * (-Random.Range (1, 4)));}

			else {m_rb2d.AddForce (transform.up * Random.Range (1, 4));}
		}

		if (Mathf.Abs (m_rb2d.velocity.x) < 1.5f && m_lastHitObj [0] != "Player" && m_lastHitObj [1] != "Player") 
		{
			if (m_rb2d.velocity.x <= 0) 
			{m_rb2d.AddForce (transform.right * (-Random.Range (1, 2)));} 

			else {m_rb2d.AddForce (transform.right * Random.Range (1, 2));}
		}
	}

	public void GoBall()
	{   
		m_rb2d.velocity = new Vector2 (0, m_speed);

		m_isShoot = true;
		gameObject.GetComponent<TrailRenderer> ().enabled = true;

		m_ballsLeft -= 1;
	}

	void InPositionBall()
	{
		m_rb2d.velocity = Vector2.zero;

		transform.position = new Vector2 (m_player.transform.position.x, m_player.transform.position.y + 0.4f);
	}

	void RespawnBall()
	{	
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		gameObject.GetComponent<TrailRenderer> ().Clear();

		InPositionBall ();
		m_isShoot = false;
	}	

	void BounceTween()
	{
		if (m_tweenIsActive) {

			if (m_currentTweenDuration >= m_bounceTweenSetting.tweenDuration) 
			{
				m_tweenIsActive = false;
				transform.localScale = m_ballStartScale;
			} 
			else 
			{	m_currentTweenDuration += Time.deltaTime;
				SetBounceTween ();
			}
		}

		SetColor ();
	}

	void SetBounceTween()
	{
		float l_tweenMoveX = Tweens.EaseOutElastic (m_currentTweenDuration, m_ballStartScale.x, m_bounceTweenSetting.ballGrowth, m_bounceTweenSetting.tweenDuration,0);
		float l_tweenMoveY = Tweens.EaseOutElastic (m_currentTweenDuration, m_ballStartScale.y, m_bounceTweenSetting.ballGrowth, m_bounceTweenSetting.tweenDuration,0);

		transform.localScale = new Vector3 (l_tweenMoveX, l_tweenMoveY);
	}
		
	void OnCollisionEnter2D(Collision2D _other)
	{
		m_lastHitObj [1] = m_lastHitObj [0];
		m_lastHitObj [0] = _other.gameObject.tag;

		if (m_isShoot) 
		{
			TweenActivator ();

			if (_other.gameObject.tag == "Block" || _other.gameObject.tag == "FixedBlock") 
			{
				SoundManager.PlaySound ("BlockHitShort");
				m_camera.GetComponent<CameraShake> ().TweenActivator ();
			}

			if (_other.gameObject.tag == "Wall") 
			{
				SoundManager.PlaySound ("WallHitShort");
			}

			if (_other.gameObject.tag == "Player") 
			{
				SoundManager.PlaySound ("VausHitShort");

				gameObject.GetComponent<Rigidbody2D> ().velocity = SetBallSpin(_other);
			}
				
			m_rb2d.velocity = m_rb2d.velocity.normalized * m_speed;
		}
	}

	public int GetBallsAmount()
	{
		return m_ballsLeft;
	}

	void SetColor()
	{
		Renderer l_rend = GetComponent<Renderer> ();

		if (m_tweenIsActive) 
		{l_rend.material.color = Color.white;} 

		else 
		{l_rend.material.color = new Color32 (254, 245, 148, 255);}
	}

	void TweenActivator() 
	{
		m_tweenIsActive = true;
		m_currentTweenDuration = 0;
	}

	Vector2 SetBallSpin(Collision2D _collider)
	{
		Vector2 l_paddlePosition = _collider.transform.position;
		Vector2 l_ballPosition = transform.position;

		Vector2 l_delta = (l_ballPosition - l_paddlePosition);
		l_delta.x = l_delta.x * 0.6f;

		Vector2 l_ballSpin = l_delta.normalized;

		return l_ballSpin;
	}

	public bool GetIsShoot()
	{
		return m_isShoot;
	}
		
}
