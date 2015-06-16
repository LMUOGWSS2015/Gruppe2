using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health = 100;

	// Use this for initialization
	void Start () {
		if (health <= 0) {
			Dead ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Dead ();
		}
	}

	void Dead(){
		Destroy (this.gameObject);
	}

	void ApplyDamage(int theDamage){
		Debug.Log("The Damage: " + theDamage);
		health -= theDamage;	
	}
}
