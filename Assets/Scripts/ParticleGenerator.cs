using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour {

	public GameObject m_particlePrefab;

	private GameObject m_particle;


	void OnCollisionEnter2D(Collision2D _other)
	{
		if (gameObject.GetComponent<BallController> ().GetIsShoot () == true) 
		{
			Vector2 l_ballPosition = transform.position;
			m_particle = Instantiate (m_particlePrefab, l_ballPosition, transform.rotation);

			SetParticleOn ();
		}
	}

	void SetParticleOn()
	{
		m_particle.GetComponent<ParticleSystem> ().Play ();

	}
}
