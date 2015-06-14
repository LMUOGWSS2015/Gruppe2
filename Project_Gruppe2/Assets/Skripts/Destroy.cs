using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public AudioClip hitSound;
	
	private AudioSource hitSource;

	// Use this for initialization
	void Start () {
		hitSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "bullet")
		{	
			// play hit sound when cube is hitted by bullet
			hitSource.PlayOneShot(hitSound, 2);

			//first destroy the bullet
			Destroy(col.gameObject);
				
			// if i am a cube, destroy me
			if(gameObject.tag == "cube"){
//				Destroy(gameObject);
			}

		}
	}
}
