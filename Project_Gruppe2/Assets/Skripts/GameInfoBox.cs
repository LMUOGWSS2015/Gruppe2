using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoBox : MonoBehaviour
{
	public static HashSet<GameInfoBoxModel> gameInfoBoxElements = new HashSet<GameInfoBoxModel> ();

	void OnGUI ()
	{
		if (gameInfoBoxElements.Count > 0) {
			DrawGameInfoBox ();
			ReduceGameInfoBoxElementsAliveTime ();
		}
	}

	[PunRPC]
	void AddKillMessage(PhotonPlayer shootPlayer, PhotonPlayer hitPlayer){
		gameInfoBoxElements.Add (new GameInfoBoxModel (0, shootPlayer.name, hitPlayer.name, "kill"));
	}

	public void DrawGameInfoBox ()
	{
		Rect rect = new Rect (Screen.width - 410, 0, 400, 500);
		GUILayout.BeginArea (rect, "box");
		
		foreach (GameInfoBoxModel gameInfoBoxModel in gameInfoBoxElements) {
			string label = "";
			switch (gameInfoBoxModel.GetAction ()) {
			case "connect":
				label += gameInfoBoxModel.GetPlayer1Name () + " HAS CONNECTED!";
				break;
			case "disconnect":
				label += gameInfoBoxModel.GetPlayer1Name () + " HAS DISCONNECTED!";
				break;
			case "kill":
				label += gameInfoBoxModel.GetPlayer1Name () + " HAS KILLED " + gameInfoBoxModel.GetPlayer2Name ();
				break;
			default:
				break;
			}
			GUILayout.BeginHorizontal ();
			GUILayout.Label (label);
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndArea ();
	}

	public void ReduceGameInfoBoxElementsAliveTime ()
	{
		HashSet<GameInfoBoxModel> temp = new HashSet<GameInfoBoxModel> (gameInfoBoxElements);
		foreach (GameInfoBoxModel gameInfoBoxModel in temp) {
			gameInfoBoxModel.AddCounter (1);
			if (gameInfoBoxModel.GetCounter () > 600) {
				gameInfoBoxElements.Remove (gameInfoBoxModel);
			}
		}
	}
}

public class GameInfoBoxModel
{
	
	private int counter = 0;
	private string player1Name;
	private string player2Name;
	private string action;
	
	public GameInfoBoxModel (int counter, string player1Name, string player2Name, string action)
	{
		this.counter = counter;
		this.player1Name = player1Name;
		this.player2Name = player2Name;
		this.action = action;
	}
	
	public void SetCounter (int counter)
	{
		this.counter = counter;
	}

	public int GetCounter ()
	{
		return this.counter;
	}

	public void AddCounter (int value)
	{
		this.counter += value;
	}
	
	public void SetPlayer1Name (string name)
	{
		this.player1Name = name;
	}

	public string GetPlayer1Name ()
	{
		return this.player1Name;
	}
	
	public void SetPlayer2Name (string name)
	{
		this.player2Name = name;
	}

	public string GetPlayer2Name ()
	{
		return this.player2Name;
	}
	
	public void SetAction (string action)
	{
		this.action = action;
	}

	public string GetAction ()
	{
		return this.action;
	}
}
