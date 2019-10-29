using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour 
{
	public int m_amountOfBonuses=12;

	public GameObject m_bonus1Prefab;
	public GameObject m_bonus2Prefab;

	public float m_chanceForBonus1;
	public float m_chanceForBonus2;

	private float m_blockAmount;
	private float m_blocksToDestroyForNextBonus;

	private List <int> m_blocksInLvl;

	void Start()
	{ 	
		m_blocksInLvl = DataController.GetLvlPattern(gameObject);
		m_blockAmount = CountBlocks();
		m_blocksToDestroyForNextBonus = Mathf.Round(m_blockAmount / m_amountOfBonuses); 

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Destructible l_objResistance = gameObject.GetComponent<Destructible> ();

		if (l_objResistance.GetHitResistance() == 0) {

			if (Destructible.GetDestroyedBlocks() == m_blocksToDestroyForNextBonus) 
			{
				Destructible.ResetDestroyedBlocks ();
			
				float rnd = Random.value;

				if (rnd <= m_chanceForBonus1) {
					Instantiate (m_bonus1Prefab, collision.transform.position, collision.transform.rotation);
				} 
				else if(rnd <= m_chanceForBonus2)
				{
					Instantiate (m_bonus2Prefab, collision.transform.position, collision.transform.rotation);
				}
			}
		} 
	}

	float CountBlocks()
	{
		float l_blockCounter = 0;

		foreach (int block in m_blocksInLvl) 
		{
			if (block == 1 || block == 2) 
			{
				l_blockCounter +=1;
			}
		}
		return l_blockCounter;

	}
}
