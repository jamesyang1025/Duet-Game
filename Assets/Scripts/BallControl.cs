using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnGamePaused;

	public AudioSource soundEffect;
	public GameObject lifeSystem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			OnGamePaused();
		}
	}

	// when the ball collide with the obstacles, player dies, trigger soundeffect
	void OnTriggerEnter2D(Collider2D col){
		soundEffect.Play();
		if(lifeSystem != null){
			lifeSystem.GetComponent<LifeSystem>().curLife -= 1;
			lifeSystem.GetComponent<LifeSystem>().UpdateLife();
			if(lifeSystem.GetComponent<LifeSystem>().curLife > 0)
				OnGamePaused();
			else
				OnPlayerDied();
			return;
		}
		OnPlayerDied();
	}
}
