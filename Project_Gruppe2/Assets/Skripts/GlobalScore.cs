using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	public static List<KeyValuePair<int, PlayerModel>> players = new List<KeyValuePair<int, PlayerModel>>();
	

	public void AddPlayer (int playerId, string playerName)
	{
		Debug.Log ("AddPlayer");
		PlayerModel player = new PlayerModel ();
		player.setPlayerId (playerId);
		player.setPlayerName (playerName);
		players.Add (new KeyValuePair<int, PlayerModel> (player.getPlayerId(),player));
	}

	public void removePlayer (int playerId)
	{
		players.Remove (players [playerId]);
	}

	public void raiseKills (int playerId, int number)
	{


	}

	public void raiseDeaths (int playerId, int number)
	{
		
		
	}
}
