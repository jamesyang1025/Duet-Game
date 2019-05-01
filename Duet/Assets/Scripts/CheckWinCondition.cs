using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinCondition : MonoBehaviour {

	public delegate void checkerDelegate();
	public static event checkerDelegate OnWin;

	private GameObject lastObstacle;

	public GameObject player;

	GameManager game;

	// Use this for initialization
	void Start () {
		game = GameManager.Instance;
		int count = gameObject.transform.childCount;
		lastObstacle = gameObject.transform.GetChild(count-1).gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if(game.GameOver)	return;

		if(lastObstacle.transform.position.y < player.transform.position.y - 5){
			OnWin();
		}

	}
}
