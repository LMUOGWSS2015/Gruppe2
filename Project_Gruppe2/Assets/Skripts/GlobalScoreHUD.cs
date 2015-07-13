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
		GUILayout.Label ("SCOREBOARD");
		
		
//		foreach(PlayerModel player in GlobalScore.players.Values){
//			GUILayout.BeginHorizontal ();
//			GUILayout.Label ("NAME");
//			GUILayout.Label (player.getPlayerName());
//			GUILayout.Label ("DEATHS");
//			GUILayout.Label (player.getDeaths().ToString());
//			GUILayout.EndHorizontal ();
//		}

		foreach(PhotonPlayer player in PhotonNetwork.playerList){
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("NAME");
			GUILayout.Label (player.name);
			GUILayout.Label ("KILLS");
			GUILayout.Label (player.customProperties["kills"].ToString());
			GUILayout.Label ("DEATHS");
			GUILayout.Label (player.customProperties["deaths"].ToString());
			GUILayout.Label ("SCORE");
			GUILayout.Label (player.GetScore().ToString());
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndArea ();
	}
}
