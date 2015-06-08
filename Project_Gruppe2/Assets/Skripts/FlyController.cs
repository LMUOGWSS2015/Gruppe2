using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour {

	public float movementSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
		} 
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
		} 
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate(Vector3.down * movementSpeed * Time.deltaTime*5);
		} 
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate(Vector3.up * movementSpeed * Time.deltaTime*5);
		} 

	}
	
}
