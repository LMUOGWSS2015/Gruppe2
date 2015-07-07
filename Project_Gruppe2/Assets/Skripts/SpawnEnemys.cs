using UnityEngine;
using System.Collections;

public class SpawnEnemys : MonoBehaviour {

	private Utils utils;
	private bool isSinglePlayer;
	private int numberEnemys = 40; 
	private ArrayList indexEnemys = new ArrayList();

	// Use this for initialization
	void Start () {

		utils = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<Utils> ();
		isSinglePlayer = Utils.isSinglePlayer;

		if (isSinglePlayer) {

			Debug.Log ("SpawnEnemys");
			SpawnSpotEnemy[] spawnSpots = GameObject.FindObjectsOfType<SpawnSpotEnemy>();
			Debug.Log (spawnSpots.Length);
			
			for(int i=0; i<numberEnemys; i++){
				
				int rand = Random.Range (0, spawnSpots.Length);
				while(indexEnemys.Contains(rand)){
					rand = Random.Range (0, spawnSpots.Length);
				}
				indexEnemys.Add(rand);

				SpawnSpotEnemy myEnemy = spawnSpots[ rand ];
				GameObject myResource = (GameObject)Resources.Load ("Sphere 1", typeof(GameObject));
				GameObject myGameObject = (GameObject)Instantiate (myResource, myEnemy.transform.position, myEnemy.transform.rotation);
			}


		}

		/*for(int i=0; i<0; i++){
		Vector3 vec = new Vector3 (Random.Range (-500, 500), Random.Range (0, 50), Random.Range (-500, 500));
	
		var hitColliders = Physics.OverlapSphere(vec, 4f);
		
			if(hitColliders.Length==0){
				Instantiate (enemy, vec, Quaternion.LookRotation (Vector3.back));
			}
		
		}*/

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
