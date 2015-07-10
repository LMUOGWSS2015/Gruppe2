using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManagerPUN : MonoBehaviour
{

	public GameObject standbyCamera;
	private int numberEnemys = 40;
	private ArrayList indexEnemys = new ArrayList ();
	private bool failed;

	private GlobalScore globalScore;
	private GameInfoBox gameInfoBox;
	private MultiplayerGameLobby mpGameLobby;
	private bool respawn = false;

	// Use this for initialization
	void Start ()
	{
		Connect ();
		globalScore = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ();
		gameInfoBox = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GameInfoBox> ();
		mpGameLobby = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<MultiplayerGameLobby> ();
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
	}

	void OnPhotonPlayerConnected (PhotonPlayer player)
	{
		GameInfoBox.gameInfoBoxElements.Add (new GameInfoBoxModel (0, player.name, "", "connect"));
	}

	public void OnPhotonPlayerDisconnected (PhotonPlayer player)
	{    
		GameInfoBox.gameInfoBoxElements.Add (new GameInfoBoxModel (0, player.name, "", "disconnect"));
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

	void OnJoinedRoom ()
	{
		MultiplayerGameLobby.showLobby = false;
		SpawnMyPlayer ();
		if (failed) {
			SpawnEnemys ();
		}
	}

	public void SpawnEnemys ()
	{
		SpawnSpotEnemy[] spawnSpots = GameObject.FindObjectsOfType<SpawnSpotEnemy> ();

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
		//instantiate a client/ player
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);

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
