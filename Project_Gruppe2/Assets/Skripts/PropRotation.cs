using UnityEngine;
using System.Collections;

public class PropRotation : MonoBehaviour {

	float speed;
	AudioSource rotorSound;
	

	// Use this for initialization
	void Start () {
		speed = 0.0f;
		rotorSound = GetComponent<AudioSource>();
		rotorSound.pitch = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (speed < 50.0f) {
			speed += 0.025f;
		}

		if (rotorSound.pitch < 2.3f) {
			rotorSound.pitch += 0.001f;
		}

		transform.Rotate(Vector3.forward * speed);
	}
}
