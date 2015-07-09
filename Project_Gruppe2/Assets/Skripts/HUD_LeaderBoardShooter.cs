﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD_LeaderBoardShooter : MonoBehaviour {

	dreamloLeaderBoard dlLeaderBoardShooter;


	// Use this for initialization
	void Start () {
		this.dlLeaderBoardShooter = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
		dlLeaderBoardShooter.LoadScores();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
			float width = 400;
			float height = 300;
			
			Rect r = new Rect ((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height);
			GUILayout.BeginArea (r, new GUIStyle ("box"));

			List<dreamloLeaderBoard.Score> scoreList = dlLeaderBoardShooter.ToListHighToLow();

			if (scoreList == null) {
				GUILayout.Label ("LOADING ...");
			} else if (scoreList.Count == 0) {
				GUILayout.Label ("NO HIGHSCORES YET!");
			}
			else {
				int maxToDisplay = 10;
				int count = 0;
				foreach (dreamloLeaderBoard.Score currentScore in scoreList) {
					count++;
					GUILayout.BeginHorizontal ();
					GUILayout.Label (currentScore.playerName);
					GUILayout.Label (currentScore.score.ToString ());
					GUILayout.EndHorizontal ();
				
					if (count >= maxToDisplay)
						break;
				}
			}
			GUILayout.EndArea ();
	}
}