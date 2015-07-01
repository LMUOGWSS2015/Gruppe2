using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GUIText text;
	static int hits = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
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
