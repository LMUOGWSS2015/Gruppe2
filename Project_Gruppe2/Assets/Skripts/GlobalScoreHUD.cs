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
		GUILayout.Label ("SCOREBOARD - " + PhotonNetwork.room.name);
		GUIStyle guiStyleLabel = new GUIStyle ();
		guiStyleLabel.fixedWidth = 75;
		guiStyleLabel.normal.textColor = Color.white;
		GUIStyle guiStyleValue = new GUIStyle ();
		guiStyleValue.fixedWidth = 200;
		guiStyleValue.normal.textColor = Color.white;

		foreach(PhotonPlayer player in PhotonNetwork.playerList){
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("NAME", guiStyleLabel);
			GUILayout.Label (player.name, guiStyleValue);
			GUILayout.Label ("KILLS", guiStyleLabel);
			GUILayout.Label (player.customProperties["kills"].ToString(), guiStyleValue);
			GUILayout.Label ("DEATHS", guiStyleLabel);
			GUILayout.Label (player.customProperties["deaths"].ToString(), guiStyleValue);
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndArea ();
	}
}
