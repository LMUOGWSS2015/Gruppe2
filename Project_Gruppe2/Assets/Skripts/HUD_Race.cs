using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_Race : MonoBehaviour
{
	int totalScore = 0;
	string playerName = "";

	public Text timerText;
	public static int score = 0;

	public float timer = 15.0f;

	dreamloLeaderBoard dl;
	
	bool gameOver;

	// Use this for initialization
	void Start ()
	{
		GameObject timeText = GameObject.Find("TimeText");
		timerText = (Text)timeText.GetComponent (typeof(Text));

		// get the reference here...
		this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

		gameOver = false;
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
			totalScore = HUD_Race.score;
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

			GUILayout.Label ("GAME OVER");
			GUILayout.Label ("Total Score: " + this.totalScore.ToString ());


			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Your Name: ");
			this.playerName = GUILayout.TextField (this.playerName);
			GUILayout.EndHorizontal ();
			if (GUILayout.Button ("Save score and go back to Main-Menu")) {	
				dl.AddScore (this.playerName, totalScore);
				Application.LoadLevel (0);
			}

			GUILayout.EndArea ();
		}
	}
}
