using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerModel
{
	public PlayerModel ()
	{
	}

	private int playerId;
	private string playerName;
	private int kills;
	private int deaths;

	public void setPlayerId (int playerId)
	{
		this.playerId = playerId;
	}
	
	public int getPlayerId ()
	{
		return this.playerId;
	}

	public void setPlayerName (string playerName)
	{
		this.playerName = playerName;
	}
	
	public string getPlayerName ()
	{
		return this.playerName;
	}
	
	public void setKills (int kills)
	{
		this.kills = kills;
	}
	
	public int getKills ()
	{
		return this.kills;
	}
	
	public void setDeaths (int deaths)
	{
		this.deaths = deaths;
	}
	
	public int getDeaths ()
	{
		return this.deaths;
	}
}

public class GlobalScore: MonoBehaviour
{

	public static Dictionary<string, PlayerModel> players = new Dictionary<string, PlayerModel>();
//	public static List<KeyValuePair<int, PlayerModel>> players = new List<KeyValuePair<int, PlayerModel>>();

	[PunRPC]
	public void RefreshData (PlayerModel player)
	{
		if(players.ContainsKey(player.getPlayerId().ToString())){
			players[player.getPlayerId().ToString()].setDeaths(player.getDeaths ());
			players[player.getPlayerId().ToString()].setKills(player.getDeaths ());

		}else{
			players.Add(player.getPlayerId().ToString(), player);
		}
	}


	[PunRPC]
	public void AddPlayer (int playerId, string playerName)
	{
		Debug.Log ("AddPlayer");
		PlayerModel player = new PlayerModel ();
		player.setPlayerId (playerId);
		player.setPlayerName (playerName);
		players.Add (player.getPlayerId().ToString(),player);

//		if (GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> () == null) {
//			Debug.LogError ("Photon View not available!");
//		} else {
//			foreach(PlayerModel p in players.Values){
//				GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> ().RPC ("RefreshData", PhotonTargets.All, player);
//			}
//
//		}
	}

	[PunRPC]
	public void RemovePlayer (int playerId)
	{
		if (players.ContainsKey (playerId.ToString())) {
			players.Remove(playerId.ToString());
		}

		if (GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> () == null) {
			Debug.LogError ("Photon View not available!");
		} else {
			GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> ().RPC ("RefreshData", PhotonTargets.All, players);
		}
	}

	[PunRPC]
	public void RaiseKills (int playerId, int number)
	{


	}

	[PunRPC]
	public void RaiseDeaths (int playerId)
	{
		Debug.Log ("RaiseDeaths");
		if (players.ContainsKey (playerId.ToString())) {
			players[playerId.ToString()].setDeaths(players[playerId.ToString()].getDeaths() + 1);
		} else {
			Debug.LogError("Raise deaths should never get called when player is unknown");
		}

		if (GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> () == null) {
			Debug.LogError ("Photon View not available!");
		} else {
			GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> ().RPC ("RefreshData", PhotonTargets.All, players);
		}
	}
}
