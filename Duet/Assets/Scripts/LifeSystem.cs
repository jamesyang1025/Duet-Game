using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour {

	public Sprite[] spriteList;
	public int curLife = 3;
	public Image life1;
	public Image life2;
	public Image life3;

	// Use this for initialization
	void Start () {
		life1.sprite = spriteList[0];
		life2.sprite = spriteList[0];
		life3.sprite = spriteList[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateLife(){
		switch(curLife){
			case 3:
				life1.sprite = spriteList[0];
				life2.sprite = spriteList[0];
				life3.sprite = spriteList[0];
				break;
			case 2:
				life1.sprite = spriteList[0];
				life2.sprite = spriteList[0];
				life3.sprite = spriteList[1];
				break;
			case 1:
				life1.sprite = spriteList[0];
				life2.sprite = spriteList[1];
				life3.sprite = spriteList[1];
				break;
			case 0:
				life1.sprite = spriteList[1];
				life2.sprite = spriteList[1];
				life3.sprite = spriteList[1];
				break;
		}
	}
}
