using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	static int hits = 0;

	public float timer = 15.0f;
	public Text timerText;

	// Use this for initialization
	void Start ()
	{
		GameObject timeText = GameObject.Find("TimeText");
		timerText = (Text)timeText.GetComponent (typeof(Text));

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

		// TODO spiel zu ende -> highscore erstellen und zurück zum mainmenü

		else {
			Time.timeScale=0.0f;

		}
	}

    public void incHits ()

	{
		Debug.Log ("Draw!");
		hits++;
		GameObject scoreGO = GameObject.Find ("ScoreText");
		Text scoreText = (Text)scoreGO.GetComponent (typeof(Text));
		scoreText.text = "Score: " + hits;

	}
}
