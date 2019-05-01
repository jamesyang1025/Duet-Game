using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public static MenuManager MenuInstance;

	public GameObject startPage;
	public GameObject playerNamePage;
	public GameObject levelPage;
	public GameObject levelPage2;
	public GameObject levelPage3;
	public GameObject levelPage4;
	public GameObject leaderboardPage;
	public InputField playerNameInput;
	public GameObject logo;
	

	enum PageState{
		None,
		Start,
		PlayerName,
		Level,
		Level2,
		Level3,
		Level4,
		Leaderboard
	}

	void Awake(){
		MenuInstance = this;
	}

	void SetPageState(PageState state){
		switch(state){
			case PageState.None:
				startPage.SetActive(false);
				playerNamePage.SetActive(false);
				levelPage.SetActive(false);
				levelPage2.SetActive(false);
				levelPage3.SetActive(false);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(false);
				logo.SetActive(false);
				break;
			case PageState.Start:
				startPage.SetActive(true);
				playerNamePage.SetActive(false);
				levelPage.SetActive(false);
				levelPage2.SetActive(false);
				levelPage3.SetActive(false);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(false);
				logo.SetActive(true);
				break;
			case PageState.PlayerName:
				startPage.SetActive(false);
				playerNamePage.SetActive(true);
				levelPage.SetActive(false);
				levelPage2.SetActive(false);
				levelPage3.SetActive(false);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(false);
				logo.SetActive(true);
				break;
			case PageState.Level:
				startPage.SetActive(false);
				playerNamePage.SetActive(false);
				levelPage.SetActive(true);
				levelPage2.SetActive(false);
				levelPage3.SetActive(false);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(false);
				logo.SetActive(true);
				break;
			case PageState.Level2:
				startPage.SetActive(false);
				playerNamePage.SetActive(false);
				levelPage.SetActive(false);
				levelPage2.SetActive(true);
				levelPage3.SetActive(false);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(false);
				logo.SetActive(true);
				break;
			case PageState.Level3:
				startPage.SetActive(false);
				playerNamePage.SetActive(false);
				levelPage.SetActive(false);
				levelPage2.SetActive(false);
				levelPage3.SetActive(true);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(false);
				logo.SetActive(true);
				break;
			case PageState.Level4:
				startPage.SetActive(false);
				playerNamePage.SetActive(false);
				levelPage.SetActive(false);
				levelPage2.SetActive(false);
				levelPage3.SetActive(false);
				levelPage4.SetActive(true);
				leaderboardPage.SetActive(false);
				logo.SetActive(true);
				break;
			case PageState.Leaderboard:
				startPage.SetActive(false);
				playerNamePage.SetActive(false);
				levelPage.SetActive(false);
				levelPage2.SetActive(false);
				levelPage3.SetActive(false);
				levelPage4.SetActive(false);
				leaderboardPage.SetActive(true);
				logo.SetActive(false);
				break;
		}
	}

	public void OnPlayGame(){
		SetPageState(PageState.PlayerName);
	}

	public void OnLevelSelected(int level){
		ProgressManager.CurLevel = level;
		if(level == 7){
			SceneManager.LoadScene("Level Endless");
		}else{
			SceneManager.LoadScene("Level " + level);
		}
	}

	public void OnPlayerNameEntered(){
		if(ProgressManager.PlayerName == ""){
			ProgressManager.PlayerName = playerNameInput.text;
		}

		if(ProgressManager.PlayerName != ""){
			SetPageState(PageState.Level);
		}
		
	}

	public void OnExitGame(){
		#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #else
         Application.Quit();
         #endif
	}

	public void BackLevelSelection(){
		ProgressManager.PlayerName = "";
		SetPageState(PageState.PlayerName);
	}

	public void BackEnterPlayerName(){
		SetPageState(PageState.Start);
	}

	public void OnShowLeaderboard(){
		SetPageState(PageState.Leaderboard);
	}

	public void BackLeaderboard(){
		SetPageState(PageState.Start);
	}

	public void NextLevelSelection(){
		SetPageState(PageState.Level2);
	}

	public void NextLevelSelection2(){
		SetPageState(PageState.Level3);
	}

	public void NextLevelSelection3(){
		SetPageState(PageState.Level4);
	}

	public void LastLevelSelection(){
		SetPageState(PageState.Level);
	}

	public void LastLevelSelection2(){
		SetPageState(PageState.Level2);
	}

	public void LastLevelSelection3(){
		SetPageState(PageState.Level3);
	}

	void Start() {
		if(ProgressManager.ReturnFromGame == true){
			ProgressManager.ReturnFromGame = false;
			OnPlayerNameEntered();
		}
	}
}
