using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject[] m_blocks;

	public Vector2 m_spawnPosition;

	public float m_xOffset = 0.1f;
	public float m_yOffset = 0.2f;


	private List <int> m_lvlPattern;

	private Vector2 m_previousPositionOfGen;

	private int m_createdBlocksInLine;

	void Start () {
		
		m_lvlPattern = DataController.GetLvlPattern(gameObject);

		transform.position = m_spawnPosition;
		m_previousPositionOfGen = transform.position;

		StartCoroutine(GenerateLevel());
	}

	IEnumerator GenerateLevel()
	{
		foreach (int block in m_lvlPattern) 
		{
				CreateBlock (block);
				MoveGenerator ();
		}
			
		yield return 0;
	}

	void MoveGenerator()
	{
		if (m_createdBlocksInLine == 10) 
		{
			transform.position = new Vector2 (m_previousPositionOfGen.x, m_previousPositionOfGen.y -(0.5f  + m_yOffset));

			m_previousPositionOfGen = transform.position;
			m_createdBlocksInLine = 0;
		} 
		else 
		{ transform.position = new Vector2 (transform.position.x +(1f+ m_xOffset), transform.position.y); }
	}

	void CreateBlock(int _blockIndex)
	{
		if (_blockIndex != 0) {
			
			Instantiate (m_blocks [_blockIndex], transform.position, transform.rotation);
		}
		m_createdBlocksInLine += 1;
	}
}