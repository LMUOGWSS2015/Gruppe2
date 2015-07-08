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
		if (health <= 0) {

			globalScore.raiseDeaths(PhotonNetwork.player.ID, 1);
			Dead ();
		}
		hud = gameObject.AddComponent<HUD> ();
		networkManager = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<NetworkManagerPUN> ();
		globalScore = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<GlobalScore> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			Dead ();
		}
	}

	void Dead ()
	{
		if (Utils.isSinglePlayer) {
			Destroy (gameObject);
		} else {
			PhotonNetwork.Destroy (gameObject);
			networkManager.SpawnMyPlayer();
		}
		hud.incHits ();
	}

	[PunRPC]
	public void ApplyDamage (int theDamage)
	{
		Debug.Log ("ApplyDamage: " + theDamage);
		hits += 1;
		health -= theDamage;

	}
}
