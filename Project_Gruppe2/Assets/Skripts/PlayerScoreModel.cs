using UnityEngine;
using System.Collections;


public class PlayerScoreModel
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public PlayerScoreModel ()
	{
	}
	
	private int kills;
	private int deaths;
	
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
