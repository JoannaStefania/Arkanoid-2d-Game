using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUpBonus : MonoBehaviour {

	public float m_maxGrowthTimes=13f;

	private Vector2 m_gravity;


	void Start()
	{
		m_gravity = Physics2D.gravity;
	}


	void FixedUpdate()
	{
		transform.Rotate (Vector3.forward * -m_gravity.y * 0.5f);
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerController l_playerController = other.gameObject.GetComponent<PlayerController> ();

			if(other.gameObject.transform.localScale.x <= l_playerController.GetPlayerGrowth()*m_maxGrowthTimes)
			{
				PlayerController.m_playerScore += l_playerController.GetPoints ("bonus");
				l_playerController.TweenActivator ("grow");
			}

			Destroy (gameObject);
		}
	}	
		

}
