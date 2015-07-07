using UnityEngine;
using System.Collections;

public class EnemyHealth : Photon.MonoBehaviour
{

	public int health = 100;
	private int hits = 0;
	private HUD hud;
	private Material mat;
	private Utils utils;

	// Use this for initialization
	void Start ()
	{
		if (health <= 0) {
			Dead ();
		}
		hud = gameObject.AddComponent<HUD> ();

		utils = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<Utils> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("Health: " + health);
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
