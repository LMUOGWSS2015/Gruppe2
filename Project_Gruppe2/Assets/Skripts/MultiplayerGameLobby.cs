using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplayerGameLobby : MonoBehaviour
{
	public static bool showLobby = false;
	float rectWidth = 800;
	float rectHeight = 500;
	string playerName = "";
	string maxPlayers = "";
	string gameName = "";
	int menuNumber = 0;
	float timerWidth = 150;
	float timerHeight = 30;
	public static float timer;
	Rect r;
	string timerText;
	GameObject standbyCamera;

	// Use this for initialization
	void Start ()
	{
		standbyCamera = GameObject.Find ("Standby Camera");
		Time.timeScale = 1.0f;
		timer = 180.0f;
	}
	
	public static void ResetTimer ()
	{
		timer = 180.0f;
	}

	[PunRPC]
	void SetRoundTimer (int playerId, float t)
	{
		if (PhotonNetwork.player.ID == playerId) {
			timer = t;
		}
	}


	// Update is called once per frame
	void Update ()
	{
		// toggle pause menu when pressing escape
		if (Input.GetKeyUp (KeyCode.Escape)) {
			menuNumber = 2;
			showLobby = !showLobby;
		}

		// update round timer
		if (timer >= 0.0f) {
			timer -= Time.deltaTime;
			timerText = "" + Mathf.RoundToInt (timer) + " sec";
		}
	}

	void OnGUI ()
	{
		GUI.skin.font = (Font)Resources.Load ("ethnocentric_rg"); 

		if (showLobby == true) {
			drawMenu ();
		} else {
			DrawHealthPoints ();
			DrawTimer ();
		}
	}

	void drawMenu ()
	{
		switch (menuNumber) {
		case 0:
			DrawMainMenu ();
			break;
		case 1:
			DrawCreateGameMenu ();
			break;
		case 2:
			DrawInGameMenu ();
			break;
		default:
			break;
		}
		
	}

	void DrawTimer ()
	{
		Rect rect = new Rect ((Screen.width / 2) - 75, 0, timerWidth, timerHeight);
		GUILayout.BeginArea (rect);
		GUILayout.BeginHorizontal ();
		GUILayout.Label (timerText);
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		
	}

	private void DrawHealthPoints ()
	{
		float rectWidth = 2300;
		float rectHeight = 30;

		Rect rect = new Rect (Screen.width - 240, Screen.height - 35, rectWidth, rectHeight);
		GUILayout.BeginArea (rect);
		GUILayout.Label ("HEALTH POINTS: " +EnemyHealth.health.ToString());
		GUILayout.EndArea ();
	}

	public void DrawMainMenu ()
	{
		GUIStyle guiStyleLabel = new GUIStyle ();
		guiStyleLabel.fixedWidth = 300;
		GUIStyle guiStyleValue = new GUIStyle ();
		guiStyleLabel.fixedWidth = 450;

		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label ("MULTIPLAYER MENU");
		
		GUILayout.BeginHorizontal ();

		GUILayout.Label ("USERNAME", GUILayout.Width(200));
		playerName = GUILayout.TextField (playerName, GUILayout.Width(550));
		GUILayout.EndHorizontal ();
		
		/* ROOM LIST -> SELECTION GRID */
		float innerRectWidth = 700;
		float innerRectHeight = 250;
		Rect roomRect = new Rect (50, (Screen.height / 2) - (innerRectHeight / 2), innerRectWidth, innerRectHeight);
		
		GUILayout.BeginArea (roomRect, new GUIStyle ("box"));
		
		GUILayout.Label ("OPEN GAMES");
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		
		foreach (RoomInfo room in rooms) {
			if (GUILayout.Button (room.name)) {
				PhotonNetwork.playerName = playerName;
				PhotonNetwork.JoinRoom (room.name);
			}
		}
		GUILayout.EndArea ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("CREATE NEW GAME", GUILayout.Width(790))) {
			menuNumber = 1;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
	}
	
	public void DrawCreateGameMenu ()
	{
		
		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label ("CREATE NEW GAME");
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("USERNAME", GUILayout.Width(200));
		playerName = GUILayout.TextField (playerName, GUILayout.Width(550));
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("GAME NAME", GUILayout.Width(200));
		gameName = GUILayout.TextField (gameName, GUILayout.Width(550));
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("START AND JOIN GAME", GUILayout.Width(790))) {
			PhotonNetwork.playerName = playerName;
			PhotonNetwork.CreateRoom (gameName);
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO MAIN MENU", GUILayout.Width(790))) {
			menuNumber = 0;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
	}
	
	public void DrawInGameMenu ()
	{	
		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label (PhotonNetwork.room.name);

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO GAME", GUILayout.Width(790))) {
			showLobby = false;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO LOBBY", GUILayout.Width(790))) {
			PhotonNetwork.LeaveRoom ();
			standbyCamera.SetActive (true);
			showLobby = true;
			menuNumber = 0;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
	}
}
