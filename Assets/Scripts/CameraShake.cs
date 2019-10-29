using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public float m_shakeDuration = 1.5f;


	private float m_currentTweenDuration;

	private bool m_tweenActive;

	private Vector3 m_cameraStartPosition;



	void Start () 
	{
		m_currentTweenDuration = 0;
		m_cameraStartPosition = transform.position;
		m_tweenActive = false;
	}

	void Update()
	{
		ShakeCameraTween ();
	}
		
	void SetShakeWave( )
	{
		float l_horizontalPosition = Tweens.ShakeWave (m_currentTweenDuration, m_shakeDuration, m_cameraStartPosition.x, 40);
		float l_verticalPosition = Tweens.ShakeWave (m_currentTweenDuration, m_shakeDuration, m_cameraStartPosition.y, 50);

		transform.position = new Vector3 (l_horizontalPosition, l_verticalPosition, transform.position.z);
	}

	void ShakeCameraTween()
	{
		if (m_tweenActive) 
		{
			if (m_currentTweenDuration >= m_shakeDuration) 
			{
				m_tweenActive = false;
			} 
			else 
			{
				m_currentTweenDuration += Time.deltaTime;

				SetShakeWave ();
			}
		}
	}

	public void TweenActivator() 
	{
		m_tweenActive = true;
		m_currentTweenDuration = 0;
	}
}
