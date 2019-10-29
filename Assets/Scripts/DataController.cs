using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController
{
	public static LevelData LoadLvlData()
	{
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Level.json");
		if (File.Exists (filePath)) {

			string jsonString = File.ReadAllText (filePath);	

			LevelData unpackedLevelData = JsonUtility.FromJson<LevelData> (jsonString);
			return unpackedLevelData;
		} 

		else
		{
			Debug.LogError ("Cannot load game data! ");

			return null;
		}
	}

	public static List<int> GetLvlPattern(GameObject _gameObj)
	{
		switch (_gameObj.scene.name) 
		{
			case "Level 1":
				return  LoadLvlData ().Level1;
			case "Level 2":
				return  LoadLvlData ().Level2;
			default:
				return null;
		}
	}

}

