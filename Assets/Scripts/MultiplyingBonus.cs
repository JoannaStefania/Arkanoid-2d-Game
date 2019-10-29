using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyingBonus : MonoBehaviour {

	public GameObject m_ballPrefab;

	private Vector2 m_gravity;



	void Start()
	{
		m_gravity = Physics2D.gravity;
	}
		
	void FixedUpdate()
	{
		transform.Rotate (Vector3.forward * m_gravity.y * 0.5f);
	}
		


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" )
		{
			PlayerController.m_playerScore += other.gameObject.GetComponent<PlayerController> ().GetPoints ("bonus");

			MultipleBall ();

			Destroy (gameObject);
		}
	}

	void MultipleBall()
	{
		GameObject l_ballObject = GameObject.FindWithTag("BallClone");

		if (l_ballObject == null) 
		{
			l_ballObject = GameObject.FindWithTag("Ball");
		}
			
		if (l_ballObject.transform.position.y > -0.65f) 
		{
			GameObject l_ballClone = Instantiate (m_ballPrefab, l_ballObject.transform.position, l_ballObject.transform.rotation);

			BallController _ball = l_ballClone.GetComponent<BallController> ();
			_ball.GoBall ();
		}

	}
		
}
