using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GlobalScore: Photon.MonoBehaviour
{

	static int count=0;

	[PunRPC]
	void RaiseKills (int playerId)
	{
		Debug.Log ("Raise Kills");	
			if(count%2==0){
			if (PhotonNetwork.player.ID == playerId) {
				Debug.Log ("killer!");
				Debug.Log ("My ID: " + PhotonNetwork.player.ID);
				Debug.Log ("Killer id: " + playerId);
				int kills = int.Parse (PhotonNetwork.player.customProperties ["kills"].ToString ()) + 1;
				ExitGames.Client.Photon.Hashtable someCustomPropertiesToSet = new ExitGames.Client.Photon.Hashtable () {{"kills", kills.ToString()}};
				PhotonNetwork.player.SetCustomProperties (someCustomPropertiesToSet);
			}
		}
		count++;

	}
}
