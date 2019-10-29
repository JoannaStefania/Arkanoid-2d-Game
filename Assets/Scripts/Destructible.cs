using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public int m_hitResistance;

	static int m_blocksDestroyed;

	private Renderer m_rend;
	private GameObject m_player;

	void Start()
	{
		m_rend = GetComponent<SpriteRenderer> ();

		m_player = GameObject.FindWithTag ("Player");
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "BallClone") 
		{
			m_hitResistance -=1;

			if (m_hitResistance == 0 )
			{
				PlayerController.m_playerScore += m_player.GetComponent<PlayerController> ().GetPoints ("block");

				m_blocksDestroyed += 1;

				Destroy (gameObject);
			}

			m_rend.material.color = ColorChange(m_hitResistance);
		}
	}

	Color32 ColorChange(int _hitResitance)
	{
		if (m_hitResistance == 1) {
			return new Color32 (253, 162, 71, 255);
		} 
		else if (m_hitResistance == 2) {
			return new Color32 (199, 18, 77, 255);
		} 
		else 
		{return m_rend.material.color;}
	}

	public int GetHitResistance()
	{
		return m_hitResistance;
	}

	public static int GetDestroyedBlocks()
	{
		return m_blocksDestroyed;
	}

	public static int ResetDestroyedBlocks()
	{
		m_blocksDestroyed = 0;
		return m_blocksDestroyed;
	}
}
