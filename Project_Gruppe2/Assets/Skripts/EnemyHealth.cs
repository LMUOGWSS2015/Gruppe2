using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health = 100;

	private int hits = 0;
	private HUD hud;

	private Material mat;

	// Use this for initialization
	void Start () {
		if (health <= 0) {
			Dead ();
		}
		hud = new HUD ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Dead ();
		}
	}

	void Dead(){
		Destroy (this.gameObject);
		hud.incHits ();

	}

	void ApplyDamage(int theDamage){
		hits += 1;
		health -= theDamage;

	}

}
