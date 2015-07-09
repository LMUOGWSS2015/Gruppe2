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
		
		
//		foreach(PlayerModel player in GlobalScore.players.Values){
//			GUILayout.BeginHorizontal ();
//			GUILayout.Label ("NAME");
//			GUILayout.Label (player.getPlayerName());
//			GUILayout.Label ("DEATHS");
//			GUILayout.Label (player.getDeaths().ToString());
//			GUILayout.EndHorizontal ();
//		}

		foreach(PhotonPlayer player in PhotonNetwork.playerList){
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("NAME");
			GUILayout.Label (player.name);
			GUILayout.Label ("KILLS");
			GUILayout.Label (player.customProperties["deaths"].ToString());
			GUILayout.Label ("DEATHS");
			GUILayout.Label (player.customProperties["deaths"].ToString());
			GUILayout.Label ("SCORE");
			GUILayout.Label (player.GetScore().ToString());
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndArea ();
	}
}
