﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUD_Race : MonoBehaviour
{
	int totalScore = 0;
	string playerName = "";

	public Text timerText;
	public static int score = 0;

	public float timer = 15.0f;

	dreamloLeaderBoard dlRace;
	
	bool gameOver;
	bool displayLeaderBoard;

	// Use this for initialization
	void Start ()
	{
		GameObject timeText = GameObject.Find("TimeText");
		timerText = (Text)timeText.GetComponent (typeof(Text));

		this.dlRace = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

		gameOver = false;
		displayLeaderBoard = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer >= 0.0f) {
			timer -= Time.deltaTime;
			if (timer <= 10.0f) {
				timerText.text = "" + Mathf.RoundToInt (timer) + " sec";
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
						Application.LoadLevel (0);
					}
				}

			}

			GUILayout.EndArea ();
		}
	}
}