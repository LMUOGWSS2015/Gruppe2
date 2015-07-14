using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManagerPUN : Photon.MonoBehaviour
{

	public GameObject standbyCamera;
	private int numberEnemys = 40;
	private ArrayList indexEnemys = new ArrayList ();
	private bool failed;
	private bool respawn = false;
	private bool joined = false;

	// Use this for initialization
	void Start ()
	{
		Connect ();
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

	void OnGUI ()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		for (int i=0; i<players.Length; i++) {
			PhotonPlayer player1 = PhotonPlayer.Find (players[i].transform.GetComponent<PhotonView> ().ownerId);
			if (player1.ID != PhotonNetwork.player.ID) {
				players [i].transform.FindChild ("Main Camera/New Text").GetComponent<TextMesh> ().text = player1.name;
			}
		}
		joined = false;
	}

	void Update ()
	{

	}

	void OnJoinedLobby ()
	{
		MultiplayerGameLobby.showLobby = true;
	}

	void OnPhotonRandomJoinFailed ()
	{
		PhotonNetwork.CreateRoom (null);
		failed = true;
	}

	void OnPhotonPlayerConnected (PhotonPlayer player)
	{	
		// only the master client sends round time to the connected player
		if (PhotonNetwork.isMasterClient) {
			Debug.Log ("MultiplayerGameLobby.ResetTimer ()");
				MultiplayerGameLobby.ResetTimer ();
			Debug.Log ("isMasterClient");
			if (GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> () == null) {
				Debug.LogError ("Photon View not available!");
			} else {
				Debug.Log ("RPC TIMER");
				GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<MultiplayerGameLobby> ().GetComponent<PhotonView> ().RPC ("SetRoundTimer", PhotonTargets.All, player.ID, MultiplayerGameLobby.timer);
			}
		}
		GameInfoBox.gameInfoBoxElements.Add (new GameInfoBoxModel (0, player.name, "", "connect"));
		joined = true;

		ExitGames.Client.Photon.Hashtable someCustomPropertiesToSet = new ExitGames.Client.Photon.Hashtable () {{"deaths", "0"}, {"kills", "0"}};
		PhotonNetwork.player.SetCustomProperties (someCustomPropertiesToSet);

	}

	public void OnPhotonPlayerDisconnected (PhotonPlayer player)
	{    
		GameInfoBox.gameInfoBoxElements.Add (new GameInfoBoxModel (0, player.name, "", "disconnect"));
	}

	void OnJoinedRoom ()
	{
		MultiplayerGameLobby.showLobby = false;

		SpawnMyPlayer ();
		MultiplayerGameLobby.ResetTimer ();

		if (failed) {
			SpawnEnemys ();
		}
	}

	public void SpawnEnemys ()
	{
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
		}

	}

	public void SpawnMyPlayer ()
	{
		//int numSpawnSpots = GameObject.FindObjectsOfType<SpawnSpotPlayer> ().Length;

		SpawnSpotPlayer[] spawnSpots = GameObject.FindObjectsOfType<SpawnSpotPlayer> ();
		//SpawnSpotPlayer spawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		int rand = Random.Range (0, spawnSpots.Length);
		SpawnSpotPlayer spawnSpot = spawnSpots [rand];

		//instantiate a client/ player
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate ("Player", spawnSpot.transform.position, spawnSpot.transform.rotation, 0);

		// set player custom properties for global scoreboard
		if (respawn == false) {
			ExitGames.Client.Photon.Hashtable someCustomPropertiesToSet = new ExitGames.Client.Photon.Hashtable () {{"deaths", "0"}, {"kills", "0"}};
			PhotonNetwork.player.SetCustomProperties (someCustomPropertiesToSet);
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
}
