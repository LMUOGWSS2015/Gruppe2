using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalScoreHUD : Photon.MonoBehaviour
{

	public static bool showScoreBoardEnd = false;
	GameObject standbyCamera;

	// Use this for initialization
	void Start ()
	{
		standbyCamera = GameObject.Find ("Standby Camera");
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		TextMesh[] texts = GameObject.FindObjectsOfType<TextMesh> ();

		for (int i=0; i<texts.Length; i++) {
			texts [i].transform.LookAt (Camera.main.transform.position, Camera.main.transform.up);
		}
	}

	void OnGUI ()
	{
		if (Input.GetKey (KeyCode.Tab)) {
			drawScoreBoard ();
		}

		if (showScoreBoardEnd == true) {
			drawScoreBoardGameEnd ();
		}
	}

	void drawScoreBoardGameEnd ()
	{
		if (PhotonNetwork.inRoom) {
			float rectWidth = 800;
			float rectHeight = 500;
		
			Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
			GUILayout.BeginArea (rect, new GUIStyle ("box"));
			GUILayout.Label ("SCOREBOARD - " + PhotonNetwork.room.name);
			GUIStyle guiStyleLabel = new GUIStyle ();
			guiStyleLabel.fixedWidth = 75;
			guiStyleLabel.normal.textColor = Color.white;
			GUIStyle guiStyleValue = new GUIStyle ();
			guiStyleValue.fixedWidth = 175;
			guiStyleValue.normal.textColor = Color.white;
		
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("NAME " + player.name, GUILayout.Width (300));
				GUILayout.Label ("DEATHS " + player.customProperties ["deaths"].ToString (), GUILayout.Width (240));
				GUILayout.Label ("KILLS " + player.customProperties ["kills"].ToString (), GUILayout.Width (240));
				GUILayout.EndHorizontal ();
			}

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("RETURN TO LOBBY", GUILayout.Width (790))) {
				showScoreBoardEnd = false;
				PhotonNetwork.LeaveRoom ();
				MultiplayerGameLobby.showLobby = true;
				MultiplayerGameLobby.menuNumber = 0;
			}
			GUILayout.EndArea ();


			GUILayout.EndHorizontal ();
		}

	}

	void drawScoreBoard ()
	{
		if (PhotonNetwork.inRoom) {
			float rectWidth = 800;
			float rectHeight = 500;
			
			Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
			GUILayout.BeginArea (rect, new GUIStyle ("box"));
			GUILayout.Label ("SCOREBOARD - " + PhotonNetwork.room.name);
			GUIStyle guiStyleLabel = new GUIStyle ();
			guiStyleLabel.fixedWidth = 75;
			guiStyleLabel.normal.textColor = Color.white;
			GUIStyle guiStyleValue = new GUIStyle ();
			guiStyleValue.fixedWidth = 175;
			guiStyleValue.normal.textColor = Color.white;
			
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("NAME " + player.name, GUILayout.Width (300));
				GUILayout.Label ("DEATHS " + player.customProperties ["deaths"].ToString (), GUILayout.Width (240));
				GUILayout.Label ("KILLS " + player.customProperties ["kills"].ToString (), GUILayout.Width (240));
				GUILayout.EndHorizontal ();
			}
			GUILayout.EndArea ();

		}

	}
}
