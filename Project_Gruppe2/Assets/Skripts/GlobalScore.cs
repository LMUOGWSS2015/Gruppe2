using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalScore: Photon.MonoBehaviour
{

	static int count=0;

	[PunRPC]
	void RaiseKills (int playerId)
	{
			if(count%2==0){
			if (PhotonNetwork.player.ID == playerId) {
				int kills = int.Parse (PhotonNetwork.player.customProperties ["kills"].ToString ()) + 1;
				ExitGames.Client.Photon.Hashtable someCustomPropertiesToSet = new ExitGames.Client.Photon.Hashtable () {{"kills", kills.ToString()}};
				PhotonNetwork.player.SetCustomProperties (someCustomPropertiesToSet);
			}
		}
		count++;

	}
}
