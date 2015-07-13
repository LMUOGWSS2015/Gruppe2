using UnityEngine;
using System.Collections;

public class MultiplayerGameLobby : MonoBehaviour
{
	public static bool showLobby = false;

	float rectWidth = 800;
	float rectHeight = 500;
	string playerName = "";
	string maxPlayers = "";
	string gameName = "";
	int menuNumber = 0;
	GameObject standbyCamera;

	// Use this for initialization
	void Start ()
	{
		standbyCamera = GameObject.Find ("Standby Camera");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// toggle pause menu when pressing escape
		if (Input.GetKeyUp (KeyCode.Escape)) {
			menuNumber = 2;
			showLobby = !showLobby;
		}

	}

	void OnGUI ()
	{
		if (showLobby) {
			drawMenu ();
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

	public void DrawMainMenu ()
	{
		
		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label ("MULTIPLAYER MENU");
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("USERNAME");
		playerName = GUILayout.TextField (playerName);
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
		if (GUILayout.Button ("CREATE NEW GAME")) {
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
		GUILayout.Label ("USERNAME");
		playerName = GUILayout.TextField (playerName);
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("GAME NAME");
		gameName = GUILayout.TextField (gameName);
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("MAXIMAL PLAYERS (20)");
		maxPlayers = GUILayout.TextField (maxPlayers);
		GUILayout.EndHorizontal ();
		
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("START AND JOIN GAME")) {
			PhotonNetwork.playerName = playerName;
			PhotonNetwork.CreateRoom (gameName);
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO MAIN MENU")) {
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
		if (GUILayout.Button ("BACK TO GAME")) {
			showLobby = false;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO LOBBY")) {
			PhotonNetwork.LeaveRoom ();
			standbyCamera.SetActive (true);
			showLobby = true;
			menuNumber = 0;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
	}
}
