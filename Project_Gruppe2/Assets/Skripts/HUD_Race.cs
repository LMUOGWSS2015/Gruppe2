using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUD_Race : MonoBehaviour
{
	int totalScore;
	string playerName;

	public Text timerText;
	public static int score;

	public float timer;

	dreamloLeaderBoard dlRace;

	float width;
	float height;
	Rect r;

	bool gameOver;
	bool displayLeaderBoard;
	bool pauseGame;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1.0f;

		width = 400;
		height = 200;
		r = new Rect ((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height);

		GameObject timeText = GameObject.Find("TimeText");
		timerText = (Text)timeText.GetComponent (typeof(Text));

		score = 0;
		totalScore = 0;
		timer = 125.0f;
		playerName = "";

		this.dlRace = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

		gameOver = false;
		displayLeaderBoard = false;
		pauseGame = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer >= 0.0f) {
			timer -= Time.deltaTime;
			if (timer <= 120.0f) {
				timerText.text = "" + Mathf.RoundToInt (timer) + " sec";
			}
			// PAUSE/RESUME GAME
			if (Input.GetKeyUp (KeyCode.Escape)) {
				if(pauseGame == false){
					Time.timeScale = 0.0f;
					pauseGame = true;
				}
				else{
					Time.timeScale = 1.0f;
					pauseGame = false;
				}
			}
		}
		else {
			// GAME OVER
			Time.timeScale=0.0f;
			totalScore = score;
			gameOver = true;
		}
	}
	
	public void incScore ()
	{
		score++;
		GameObject scoreGO = GameObject.Find ("ScoreText");
		Text scoreText = (Text)scoreGO.GetComponent (typeof(Text));
		scoreText.text = "Score: " + score;
	}

	void OnGUI()
	{
		if (pauseGame) {
			width = 400;
			height = 200;
			r = new Rect ((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height);
			GUILayout.BeginArea (r, new GUIStyle ("box"));
			GUILayout.Label ("RACE | PAUSE");
			if (GUILayout.Button("Back to Main-Menu"))
			{
				GoBackToMainMenu();
			}
			GUILayout.EndArea ();
		}

		if (gameOver) {
			float width = 400;
			float height = 200;
		
			Rect r = new Rect ((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height);
			GUILayout.BeginArea (r, new GUIStyle ("box"));

			GUILayout.Label ("RACE | GAME OVER");
			GUILayout.Label ("Total Score: " + this.totalScore.ToString ());

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Your Name: ");
			this.playerName = GUILayout.TextField (this.playerName);
			GUILayout.EndHorizontal ();

			if(displayLeaderBoard == false){
				if (GUILayout.Button ("Save Score")) {	
					dlRace.AddScore (this.playerName, totalScore);
					displayLeaderBoard = true;
				}
			}

			if (displayLeaderBoard == true) {
				List<dreamloLeaderBoard.Score> scoreList = dlRace.ToListHighToLow();
				if (scoreList == null) 
				{
					GUILayout.Label("(loading...)");
				} 
				else 
				{
					if (GUILayout.Button("Back to Main-Menu"))
					{
						GoBackToMainMenu();
					}
				}

			}

			GUILayout.EndArea ();
		}
	}

	public void GoBackToMainMenu(){
		Menu2 menuManager = GameObject.Find("_MenuManager").GetComponent<Menu2>();
		menuManager.ChangeToScene(0);
	}
}
