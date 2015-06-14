using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bullet")
		{	
			//first destroy the bullet
			Destroy(col.gameObject);
			Debug.Log ("Bullet destroyed!");
				
			// if i am a cube, destroy me
			if(gameObject.tag == "cube"){
				Destroy(gameObject);
			}

		}
	}
}
