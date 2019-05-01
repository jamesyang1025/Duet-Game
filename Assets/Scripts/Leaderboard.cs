using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	private string[] playerNames = new string[5];
	private int[] playerScores = new int[5];

	public List<Text> playerNamesText;
	public List<Text> playerScoresText;

	// Use this for initialization
	void Start () {

		for(int i = 0; i < 5; i++){
			playerNames[i] = PlayerPrefs.GetString("player " + i + " name", "N/A");
			playerScores[i] = PlayerPrefs.GetInt("player " + i + " score", 0);
		}
		

		for(int i = 0; i < 5; i++){
			playerNamesText[i].text = playerNames[i];
			playerScoresText[i].text = playerScores[i].ToString();
			
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
