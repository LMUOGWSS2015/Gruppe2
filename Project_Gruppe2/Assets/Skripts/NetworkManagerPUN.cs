using UnityEngine;
using System.Collections;

public class NetworkManagerPUN : MonoBehaviour {

	public Camera standbyCamera;
	
	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	void Connect () {
		PhotonNetwork.ConnectUsingSettings ("Multuplayer Game Of Drones 0.1");
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

	void SpawnMyPlayer(){
		Debug.Log ("SpawnMyPlayer");

		//instantiate a client/ player
		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);

		// enable shooting + fly controller per instance
		myPlayer.GetComponent<RaycastShooting> ().enabled = true;
		myPlayer.GetComponent<FlyController> ().enabled = true;

		// enable cameras per instance
		Transform mainCamera = myPlayer.transform.FindChild ("Main Camera");
		mainCamera.GetComponent<Camera> ().enabled = true;
		mainCamera.FindChild ("Camera").GetComponent<Camera> ().enabled = true;

		// disable standby camera when game starts
		standbyCamera.enabled = false;
	}
}
