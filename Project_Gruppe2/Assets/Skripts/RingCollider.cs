using UnityEngine;
using System.Collections;

public class RingCollider : MonoBehaviour {

	private HUD_Race hudRace;


	// Use this for initialization
	void Start () {
		hudRace = gameObject.AddComponent<HUD_Race> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){
		hudRace.incScore ();
		Destroy(gameObject);
	}
}
