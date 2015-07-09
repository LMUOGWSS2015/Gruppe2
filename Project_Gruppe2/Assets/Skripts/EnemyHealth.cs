using UnityEngine;
using System.Collections;

public class EnemyHealth : Photon.MonoBehaviour
{

	public int health = 100;
	private int hits = 0;
	private HUD hud;
	private Material mat;
	private NetworkManagerPUN networkManager;
	private GlobalScore globalScore;

	// Use this for initialization
	void Start ()
	{
		networkManager = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<NetworkManagerPUN> ();
		globalScore = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ();
	}
	
	// Update is called once per frame
	void Update (){

		if (health <= 0) {
			int deaths = int.Parse(PhotonNetwork.player.customProperties["deaths"].ToString()) + 1;
			PhotonNetwork.player.customProperties["deaths"] =  deaths.ToString();
			Dead ();
		}
	}

	void Dead ()
	{
		if (Utils.isSinglePlayer) {
			Destroy (gameObject);
		} else {
			PhotonNetwork.Destroy (gameObject);
			if(Utils.isSinglePlayer){
				networkManager.SpawnEnemys();
			}else{
				networkManager.SpawnMyPlayer();
			}

		}
		//hud.incHits ();
	}

	[PunRPC]
	public void ApplyDamage (int theDamage)
	{
		Debug.Log ("ApplyDamage: " + theDamage);
		hits += 1;
		health -= theDamage;

	}
}
