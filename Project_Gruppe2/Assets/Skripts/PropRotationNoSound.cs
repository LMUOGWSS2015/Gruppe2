using UnityEngine;
using System.Collections;

public class PropRotationNoSound : MonoBehaviour {
	
	float speed;
	
	
	// Use this for initialization
	void Start () {
		speed = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (speed < 50.0f) {
			speed += 0.025f;
		}
		transform.Rotate(Vector3.forward * speed);
	}
}