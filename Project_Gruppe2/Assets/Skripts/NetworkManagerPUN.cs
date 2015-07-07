using UnityEngine;
using System.Collections;

public class NetworkManagerPUN : MonoBehaviour {

	public GameObject standbyCamera;
	private int numberEnemys = 40; 
	private ArrayList indexEnemys = new ArrayList();

	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	void Connect () {
		if (Utils.offlineMode) {
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
		} else {
			PhotonNetwork.ConnectUsingSettings ("Multiplayer Game Of Drones 0.1");
		}
	}
	
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}

	void SpawnEnemys(){
		Debug.Log ("SpawnEnemys");
		SpawnSpotEnemy[] spawnSpots = GameObject.FindObjectsOfType<SpawnSpotEnemy>();
		Debug.Log (spawnSpots.Length);

		for(int i=0; i<numberEnemys; i++){

			int rand = Random.Range (0, spawnSpots.Length);
			while(indexEnemys.Contains(rand)){
				rand = Random.Range (0, spawnSpots.Length);
			}
			indexEnemys.Add(rand);

			SpawnSpotEnemy myEnemy = spawnSpots[ rand ];
			PhotonNetwork.Instantiate("Sphere 1", myEnemy.transform.position, myEnemy.transform.rotation, 0);
		}

	}

	void SpawnMyPlayer(){
		Debug.Log ("SpawnMyPlayer");

		//instantiate a client/ player
		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);

		// enable everything which sould only run once per instance
		myPlayer.GetComponent<RaycastShooting> ().enabled = true;
		myPlayer.GetComponent<FlyController> ().enabled = true;
		myPlayer.GetComponent<EnemyHealth> ().enabled = true;
		myPlayer.GetComponent<AudioSource> ().enabled = true;
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
	}
}
