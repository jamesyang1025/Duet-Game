using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

	public float speed;
	public float rotateSpeed;

	private Vector3 startPosition;
	private Quaternion startRotation;
	GameManager game;

	public bool vanisher;
	public bool shifter;
	public bool rotater;

	public float minX;
	public float maxX;
	private bool movingRight;

	// Use this for initialization
	void Start () {
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
		game = GameManager.Instance;
		if(startPosition.x < 0){
			movingRight = true;
		}else{
			movingRight = false;
		}
	}

	// Register events
	void OnEnable(){
		GameManager.OnGameStarted += OnGameStarted;
		GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	// De-register events
	void OnDisable(){
		GameManager.OnGameStarted -= OnGameStarted;
		GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	// when game start, reset gameobject position
	void OnGameStarted(){
		gameObject.transform.position = startPosition;
		gameObject.transform.rotation = startRotation;
	}

	// when gameOver confirmed, restart the game, reset the position and rotation
	void OnGameOverConfirmed(){
		gameObject.transform.position = startPosition;
		gameObject.transform.rotation = startRotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(game.GameOver)	return;

		// gameobject moving down at constant speed
		gameObject.transform.position += Vector3.down * speed;
		
		VanisherControl();
		ShifterControl();
		RotaterControl();
	}

	void VanisherControl(){
		if(vanisher){
			GameObject player = GameObject.Find("Player");
			if(Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= 5){
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
			}else{
				gameObject.GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}

	void ShifterControl(){
		if(shifter){
			GameObject player = GameObject.Find("Player");
			if(Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= 5){
				// obstacles in right half move left
				if(!movingRight && gameObject.transform.position.x < maxX){
					gameObject.transform.position += Vector3.left * speed;
				}

				// obstacles in left half move right
				if(movingRight && gameObject.transform.position.x > minX){
					gameObject.transform.position += Vector3.right * speed;
				}
				
			}
			

		}
	}

	void RotaterControl(){
		if(rotater){
			transform.Rotate(0.0f, 0.0f, rotateSpeed, Space.Self);
		}
	}

	


	
}
