using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManagerPUN : MonoBehaviour
{

	public GameObject standbyCamera;
	private int numberEnemys = 40;
	private ArrayList indexEnemys = new ArrayList ();
	private bool failed;
	private bool showMenu = false;
	private GlobalScore globalScore;

	private bool respawn = false;

	// Use this for initialization
	void Start ()
	{
		Connect ();
		globalScore = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ();
	}
	
	void Connect ()
	{
		if (Utils.offlineMode) {
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby ();
		} else {
			PhotonNetwork.ConnectUsingSettings ("Multiplayer Game Of Drones 0.1");
		}
	}

	float rectWidth = 800;
	float rectHeight = 500;
	string playerName = "";
	string maxPlayers = "";
	string gameName = "";
	int menuNumber = 0;
	
	void OnGUI ()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

		if (showMenu) {
			drawMenu ();
		}

	}

	void Update ()
	{


		if (Input.GetKey (KeyCode.Escape)) {
			menuNumber = 2;
			showMenu = !showMenu;
		}

	}

	void OnJoinedLobby ()
	{
		Debug.Log ("OnJoinedLobby");
		showMenu = true;
	}

	void OnPhotonRandomJoinFailed ()
	{
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
		failed = true;
	}

	void OnJoinedRoom ()
	{
		showMenu = false;
//		if (GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> () == null) {
//			Debug.LogError ("Photon View not available!");
//		} else {
//			GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> ().RPC ("AddPlayer", PhotonTargets.All, PhotonNetwork.player.ID, PhotonNetwork.player.name);
//		}
		//globalScore.AddPlayer (PhotonNetwork.player.ID, PhotonNetwork.player.name);
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
		if (failed) {
			SpawnEnemys ();
		}
	}

	public void SpawnEnemys ()
	{
		Debug.Log ("SpawnEnemys");
		SpawnSpotEnemy[] spawnSpots = GameObject.FindObjectsOfType<SpawnSpotEnemy> ();
		Debug.Log (spawnSpots.Length);

		for (int i=0; i<numberEnemys; i++) {

			int rand = Random.Range (0, spawnSpots.Length);
			while (indexEnemys.Contains(rand)) {
				rand = Random.Range (0, spawnSpots.Length);
			}
			indexEnemys.Add (rand);

			SpawnSpotEnemy myEnemy = spawnSpots [rand];

			GameObject myResource = (GameObject)Resources.Load ("Sphere 1", typeof(GameObject));
			Instantiate (myResource, myEnemy.transform.position, myEnemy.transform.rotation);

			//PhotonNetwork.Instantiate ("Sphere 1", myEnemy.transform.position, myEnemy.transform.rotation, 0);
		}

	}

	public void SpawnMyPlayer ()
	{
		Debug.Log ("SpawnMyPlayer");

		//instantiate a client/ player
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);

		// set player custom properties for global scoreboard
		if (respawn == false) {
			ExitGames.Client.Photon.Hashtable someCustomPropertiesToSet = new ExitGames.Client.Photon.Hashtable() {{"deaths", "0"}, {"kills", "0"}};
			PhotonNetwork.player.SetCustomProperties(someCustomPropertiesToSet);
		}
	
		// enable everything which sould only run once per instance
		myPlayer.GetComponent<RaycastShooting> ().enabled = true;
		myPlayer.GetComponent<FlyController> ().enabled = true;
		myPlayer.GetComponent<EnemyHealth> ().enabled = true;
		myPlayer.GetComponent<AudioSource> ().enabled = true;
		//((MonoBehaviour)myPlayer.GetComponent ("Timer")).enabled = true;
		myPlayer.transform.Find ("Main Camera/Camera/Drone/QuadCopter").GetComponent<LightControl> ().enabled = true;
		myPlayer.transform.Find ("Main Camera/Camera/Drone/QuadCopter/PropRR").GetComponent<AudioSource> ().enabled = true;

		// enable cameras per instance
		myPlayer.transform.Find ("Main Camera").GetComponent<Camera> ().enabled = true;
		myPlayer.transform.Find ("Main Camera").GetComponent<AudioListener> ().enabled = true;
		myPlayer.transform.Find ("Main Camera/Camera").GetComponent<Camera> ().enabled = true;

		myPlayer.GetComponent<NetworkCharacter> ().enabled = true;

		// disable standby camera when game starts
		standbyCamera.SetActive (false);
		standbyCamera.GetComponent<AudioListener> ().enabled = false;

		respawn = true;
	}

	void drawMenu ()
	{
		switch (menuNumber) {
		case 0:
			drawMainMenu ();
			break;
		case 1:
			drawCreateGameMenu ();
			break;
		case 2:
			drawInGameMenu ();
			break;
		default:
			break;
		}
		
	}
	
	void drawMainMenu ()
	{
		
		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label ("MULTIPLAYER MENU");
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Please enter username");
		playerName = GUILayout.TextField (playerName);
		GUILayout.EndHorizontal ();
		
		/* ROOM LIST -> SELECTION GRID */
		float innerRectWidth = 700;
		float innerRectHeight = 250;
		Rect roomRect = new Rect (50, (Screen.height / 2) - (innerRectHeight / 2), innerRectWidth, innerRectHeight);
		
		GUILayout.BeginArea (roomRect, new GUIStyle ("box"));
		
		GUILayout.Label ("OPEN GAMES");
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		//string[] rooms = new string[] {"HALLO", "WAS GEHT"};
		foreach (RoomInfo room in rooms) {
			if (GUILayout.Button (room.name)) {
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
	
	void drawCreateGameMenu ()
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
	
	void drawInGameMenu ()
	{
		
		Rect rect = new Rect ((Screen.width / 2) - (rectWidth / 2), (Screen.height / 2) - (rectHeight / 2), rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.Label (PhotonNetwork.room.name);
		
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO GAME")) {
			showMenu = false;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("BACK TO LOBBY")) {
			PhotonNetwork.LeaveRoom ();
			standbyCamera.SetActive (true);
			showMenu = true;
			menuNumber = 0;
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
	}
}
