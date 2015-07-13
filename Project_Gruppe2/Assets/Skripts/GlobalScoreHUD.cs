using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalScoreHUD : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		TextMesh[] texts = GameObject.FindObjectsOfType<TextMesh>();

		Debug.Log ("Change Text: "+texts.Length);

		for(int i=0; i<texts.Length; i++){

			texts[i].transform.LookAt(Camera.main.transform.position, Camera.main.transform.up);

		}
	
		/*GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		
		Debug.Log ("Players Length: " + players.Length);
		for (int i=0; i<players.Length; i++) {
			PhotonPlayer player = PhotonPlayer.Find (players[i].transform.GetComponent<PhotonView>().ownerId);
			players[i].transform.FindChild("Main Camera/Camera/New Text").GetComponent<TextMesh>().text = player.name;
			//if(player != null){
			//Debug.Log ("PlayersName: " + player.name);
			//}
		}*/

		/*for(GameObject player in GameObject.FindGameObjectsWithTag ("Player")){

		};*/


		//PhotonPlayer player = PhotonPlayer.Find(GameObject.GetComponent<PhotonView>().ownerId);

		

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
