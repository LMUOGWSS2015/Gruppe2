using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalScoreHUD : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (Input.GetKey (KeyCode.Tab)) {
			drawScoreBoard();
		}
	}

	void drawScoreBoard ()
	{
		float rectWidth = 800;
		float rectHeight = 500;
		
		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label ("SCOREBOARD");
		
		
		foreach(KeyValuePair<int, PlayerModel> player in GlobalScore.players){
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("NAME");
			GUILayout.Label (player.Value.getPlayerName());
			GUILayout.Label ("DEATHS");
			GUILayout.Label (player.Value.getDeaths().ToString());
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndArea ();
	}
}
