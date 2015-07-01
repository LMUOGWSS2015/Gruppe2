using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GUIText text;
<<<<<<< Updated upstream
	static int hits = 0;
=======
	public float timer = 185.0f;
	Text guiText;
>>>>>>> Stashed changes

	// Use this for initialization
	void Start ()
	{
		GameObject timeText = GameObject.Find("TimeText");
		guiText = (Text)timeText.GetComponent (typeof(Text));
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer >= 0.0f ) {
			timer -= Time.deltaTime;
			if(timer <= 180.0f){
			guiText.text = " " + Mathf.RoundToInt(timer) + " sec";
			}
		}

	
	}

	public void incHits ()
	{
		Debug.Log ("Draw!");
		hits++;
		GameObject scoreGO = GameObject.Find ("ScoreText");
		Text guiText = (Text)scoreGO.GetComponent (typeof(Text));
		guiText.text = "Treffer: " + hits;

	}
}
