using UnityEngine;
using System.Collections;

public class EnemyHealth : Photon.MonoBehaviour
{

	public static int health = 100;
	private int hits = 0;
	private HUD hud;
	private Material mat;
	private NetworkManagerPUN networkManager;

	// Use this for initialization
	void Start ()
	{
		networkManager = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<NetworkManagerPUN> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (isAlive () == false) {
//			if (Utils.isSinglePlayer == false) {
//				Debug.Log ("Is Dead!");
//				int deaths = int.Parse (PhotonNetwork.player.customProperties ["deaths"].ToString ()) + 1;
//				ExitGames.Client.Photon.Hashtable someCustomPropertiesToSet = new ExitGames.Client.Photon.Hashtable () {{"deaths", deaths.ToString()}};
//				PhotonNetwork.player.SetCustomProperties (someCustomPropertiesToSet);
//				Debug.Log ("deaths: " + PhotonNetwork.player.customProperties ["deaths"]);
//			}
//			Dead ();
//		}
	}

	void Dead ()
	{
		if (Utils.isSinglePlayer) {
			Destroy (gameObject);
		} else {
			PhotonNetwork.Destroy (gameObject);
			if (Utils.isSinglePlayer) {
				networkManager.SpawnEnemys ();
			} else {
				networkManager.SpawnMyPlayer ();
				health = 100;
			}

		}
		//hud.incHits ();
	}

	[PunRPC]
	public void ApplyDamage (int theDamage, PhotonPlayer shootPlayer, PhotonPlayer hitPlayer)
	{
		Debug.Log ("Apply Damage");
		// only apply damage if was hitten
		if (hitPlayer.ID == PhotonNetwork.player.ID) {
			hits += 1;
			health -= theDamage;
			Debug.Log ("Health: " + health);
		}

		if (isAlive () == false) {
			Debug.Log ("Apply Damage - isDead");

			int deaths = int.Parse (PhotonNetwork.player.customProperties ["deaths"].ToString ()) + 1;
			PhotonNetwork.player.SetCustomProperties (new ExitGames.Client.Photon.Hashtable () {{"deaths", deaths.ToString()}});

			if (GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> () == null) {
				Debug.LogError ("Photon View not available!");
			} else {
				GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView> ().RPC ("RaiseKills", PhotonTargets.All, shootPlayer.ID);
				GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GameInfoBox> ().GetComponent<PhotonView> ().RPC ("AddKillMessage", PhotonTargets.All, shootPlayer, hitPlayer);
			}

			Dead ();
		}

	}

	public void ApplyDamage2 (int theDamage)
	{
		Debug.Log ("ApplyDamage: " + theDamage);
		hits += 1;
		health -= theDamage;

		if (isAlive () == false) {
			hud.incHits();
			Dead ();
		}
		
		/*if (isAlive () == false) {
			Debug.Log("I am dead...");
			if (GameObject.Find("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView>() == null) {
				Debug.LogError ("Photon View not available!");
			} else {
				Debug.Log("and raise kills");
				GameObject.Find("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ().GetComponent<PhotonView>().RPC ("RaiseKills", PhotonTargets.All, playerId);
			}
		}*/
	}

	bool isAlive ()
	{
		if (health <= 0) {
			return false;
		} else {
			return true;
		}
	}
}
