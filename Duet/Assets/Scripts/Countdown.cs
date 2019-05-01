using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Countdown : MonoBehaviour {

	public delegate void CountdownFinished();
	public static event CountdownFinished OnCountdownFinished;

	Text countdown;
	
	// when the gameobject is enabled, start countdown
	void OnEnable(){
		countdown = GetComponent<Text>();
		countdown.text = "3";
		StartCoroutine(CountDown());
	}

	// countdown function
	IEnumerator CountDown(){
		int count = 3;
		for(int i = 0; i < count; i++){
			countdown.text = (count - i).ToString();
			yield return new WaitForSeconds(1);
		}

		OnCountdownFinished();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
