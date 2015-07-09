using UnityEngine;
using System.Collections;

public class SpawnSpotRingMap : MonoBehaviour {

	private int numberRings = 40;
	private ArrayList indexRings = new ArrayList ();
	
	
	// Use this for initialization
	void Start () {
		
		GenerateSpawnRings ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void GenerateSpawnRings ()
	{
		Debug.Log ("SpawnRings");
		SpawnSpotRing[] spawnSpots = GameObject.FindObjectsOfType<SpawnSpotRing> ();
		Debug.Log (spawnSpots.Length);
		

		for (int i=0; i<numberRings; i++) {

			Debug.Log ("Create SpawnRing");

			
			int rand = Random.Range (0, spawnSpots.Length);
			while (indexRings.Contains(rand)) {
				rand = Random.Range (0, spawnSpots.Length);
			}
			indexRings.Add (rand);
			
			SpawnSpotRing myEnemy = spawnSpots [rand];

			Vector3 _tmp = myEnemy.transform.position; // getter
			_tmp.y = Random.Range (4,60);; // change 'NEW' Vector3
			myEnemy.transform.position = _tmp; // change Transform.position with it's setter

			GameObject myResource = (GameObject)Resources.Load ("Ring", typeof(GameObject));
			GameObject myGameObject = null;
			myGameObject = (GameObject)Instantiate (myResource, myEnemy.transform.position, myEnemy.transform.rotation);
		}
		
	}

}
