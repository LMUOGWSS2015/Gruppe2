using UnityEngine;
using System.Collections;

public class SpawnEnemys : MonoBehaviour {

	public GameObject enemy;

	// Use this for initialization
	void Start () {


		for(int i=0; i<50; i++){
		Vector3 vec = new Vector3 (Random.Range (-500, 500), Random.Range (0, 50), Random.Range (-500, 500));
	
		var hitColliders = Physics.OverlapSphere(vec, 4f);
		
			if(hitColliders.Length==0){
				Instantiate (enemy, vec, Quaternion.LookRotation (Vector3.back));
			}
		
		}

		/*function ExplosionDamage(center: Vector3, radius: float) {
			var hitColliders = Physics.OverlapSphere(center, radius);
			
			for (var i = 0; i < hitColliders.Length; i++) {
				hitColliders[i].SendMessage("AddDamage");
			}
		}*/

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
