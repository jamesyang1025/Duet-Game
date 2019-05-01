using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;

	public static GameManager Instance;

	public GameObject gameOverPage;
	public GameObject countdownPage;
	public GameObject winPage;
	public GameObject pausePage;
	public Text scoreText;

	// manage the game state
	enum PageState{
		None,
		Start,
		GameOver,
		Countdown,
		Win,
		Pause
	}

	int score = 0;
	bool gameOver = true;
	bool gamePaused = false;

	// Get the gameover value
	public bool GameOver{
		get { return gameOver; } 
	}

	// Gameobject awakes
	void Awake(){
		Instance = this;
	}

	// Register the events
	void OnEnable(){
		Countdown.OnCountdownFinished += OnCountdownFinished;
		BallControl.OnPlayerDied += OnPlayerDied;
		BallControl.OnGamePaused += OnGamePaused;
		CheckWinCondition.OnWin += OnWin;
	}

	// De-register the events
	void OnDisable(){
		Countdown.OnCountdownFinished -= OnCountdownFinished;
		BallControl.OnPlayerDied -= OnPlayerDied;
		BallControl.OnGamePaused -= OnGamePaused;
		CheckWinCondition.OnWin -= OnWin;
	}

	void OnWin(){
		SetPageState(PageState.Win);
		gameOver = true;
	}

	// When countdown finished, start the game
	void OnCountdownFinished(){
		SetPageState(PageState.None);
		if(!gamePaused){
			OnGameStarted();
			score = 0;
		}else{
			gamePaused = false;
		}
		gameOver = false;
	}

	// Player died, enable gameover page
	void OnPlayerDied(){
		gameOver = true;
		SetPageState(PageState.GameOver);
	}

	void OnGamePaused(){
		gameOver = true;
		gamePaused = true;
		SetPageState(PageState.Pause);
	}



	// Set the game state
	void SetPageState(PageState state){
		switch(state){
			case PageState.None:
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				winPage.SetActive(false);
				pausePage.SetActive(false);
				break;
			case PageState.Win:
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				winPage.SetActive(true);
				pausePage.SetActive(false);
				break;
			case PageState.GameOver:
				gameOverPage.SetActive(true);
				countdownPage.SetActive(false);
				winPage.SetActive(false);
				pausePage.SetActive(false);
				break;
			case PageState.Countdown:
				gameOverPage.SetActive(false);
				countdownPage.SetActive(true);
				winPage.SetActive(false);
				pausePage.SetActive(false);
				break;
			case PageState.Pause:
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				winPage.SetActive(false);
				pausePage.SetActive(true);
				break;

		}
	}

	public void ConfirmedResume(){
		SetPageState(PageState.Countdown);
	}

	// Confirmed the gameover, start coundown state
	public void ConfirmedGameOver(){
		gamePaused = false;
		OnGameOverConfirmed();
		SaveData(ProgressManager.PlayerName.ToUpper(), int.Parse(Regex.Match(scoreText.text, @"\d+").Value));
		scoreText.text = "Score: " + score;
		SetPageState(PageState.Countdown);
	}

	// Quit the game, return to main menu
	public void Quit(){
		ProgressManager.ReturnFromGame = true;
		SaveData(ProgressManager.PlayerName.ToUpper(), int.Parse(Regex.Match(scoreText.text, @"\d+").Value));
		SceneManager.LoadScene("UIMenu");
	}

	private void SaveData(string playerName, int playerScore){
		Dictionary<string, int> playerData = new Dictionary<string, int>();

		for(int i = 0; i < 5; i++){
			string name = PlayerPrefs.GetString("player " + i + " name", "N/A");
			int score = PlayerPrefs.GetInt("player " + i + " score", 0);
			if(name != "N/A")
				playerData.Add(name, score);
		}

		if(playerData.ContainsKey(playerName)){
			playerData[playerName] = Mathf.Max(playerData[playerName], playerScore);
		}else{
			playerData.Add(playerName, playerScore);
		}


		int j = 0;
		foreach(KeyValuePair<string, int> player in playerData.OrderByDescending(key => key.Value)){
			PlayerPrefs.SetString("player " + j + " name", player.Key);
			PlayerPrefs.SetInt("player " + j + " score", player.Value);
			j++;
		}

		PlayerPrefs.Save();


	}

	

}
