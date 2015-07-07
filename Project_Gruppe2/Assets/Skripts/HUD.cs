using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GUIText text;
	static int hits = 0;

	public float timer = 15.0f;
	Text guiText;

	// Use this for initialization
	void Start ()
	{
		GameObject timeText = GameObject.Find("TimeText");
		guiText = (Text)timeText.GetComponent (typeof(Text));

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (timer >= 0.0f) {
			timer -= Time.deltaTime;
			if (timer <= 10.0f) {
				guiText.text = "" + Mathf.RoundToInt (timer) + " sec";
			}
	
		}

		// TODO spiel zu ende -> highscore erstellen und zurück zum mainmenü

		else {


		}
	}

    public void incHits ()

	{
		Debug.Log ("Draw!");
		hits++;
		GameObject scoreGO = GameObject.Find ("ScoreText");
		Text guiText = (Text)scoreGO.GetComponent (typeof(Text));
		guiText.text = "Score: " + hits;

	}
}
