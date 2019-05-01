using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {


	public float speed;

	public GameObject gameOver;

	private Vector3 startPosition = new Vector3(0, -3, 0);

	GameManager game;

	public Text scoreText;

	private float score;
	private int newlyEarnedScore;

	public GameObject lifeSystem;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = startPosition;
		gameOver.SetActive(false);
		game = GameManager.Instance;
		newlyEarnedScore = 0;
	}

	// Register the events
	void OnEnable(){
		GameManager.OnGameStarted += OnGameStarted;
		GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	// De-register the events
	void OnDisable(){
		GameManager.OnGameStarted -= OnGameStarted;
		GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	// when game start, reset gameobject position and rotation
	void OnGameStarted(){
		gameObject.transform.position = startPosition;
		gameObject.transform.rotation = Quaternion.identity;
		score = 0;
		if(lifeSystem != null){
			lifeSystem.GetComponent<LifeSystem>().curLife = 3;
			lifeSystem.GetComponent<LifeSystem>().UpdateLife();
		}
	}

	// when gameOver confirmed, restart the game, reset the position and rotation
	void OnGameOverConfirmed(){
		gameObject.transform.position = startPosition;
		gameObject.transform.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {

		if(game.GameOver)	return;

		//rotate with direction keys
		transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * speed);

		score += Time.deltaTime;
		scoreText.text = "Score: " + (int)(score * 10 * ProgressManager.CurLevel);
		newlyEarnedScore = (int)(score * 10 * ProgressManager.CurLevel) % 500;
		CheckScoreLife();
	}

	void CheckScoreLife(){
		if(lifeSystem != null){
			if(lifeSystem.GetComponent<LifeSystem>().curLife < 3){
				if(newlyEarnedScore == 499){
					lifeSystem.GetComponent<LifeSystem>().curLife++;
					lifeSystem.GetComponent<LifeSystem>().UpdateLife();
				}
			}
		}
	}

}
