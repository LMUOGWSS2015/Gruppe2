using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour {
	
	public Transform drone;
	
	public float movementSpeed = 0.5f;
	private float rotationSpeedDefault = 0.5f;
	private float rotationSpeedMax = 4.5f;
	private float rotationAcc = 1.000001f;


	private float rotationSpeed = 0.5f;
	
	public float controllerSensitivity = 0.5f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		////////////
		// KEYBOARD
		///////////
		
		// hoch
		if (Input.GetKey (KeyCode.W)) {
			GoUp();
		}
		
		// runter
		if (Input.GetKey (KeyCode.S)) {
			GoDown();
		}
		
		// neigung links
		if (Input.GetKey (KeyCode.A)) {
			GoLeft(1f);
		}
		
		// neigung rechts
		if (Input.GetKey (KeyCode.D)) {
			GoRight(-1f);
		}
		
		// neigung vor
		if (Input.GetKey (KeyCode.UpArrow)) {
			GoForward(1f);
		}
		
		// neigung zurück
		if (Input.GetKey (KeyCode.DownArrow)) {
			GoBack(-1f);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow) ) {
			rotationSpeed = rotationSpeedDefault;
			rotationAcc = 1.000001f;
		}


		// drehung links
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if(rotationSpeed < rotationSpeedMax){
			rotationSpeed = rotationSpeed * rotationAcc;
				if(rotationAcc < 1.2f){
					rotationAcc = rotationAcc * rotationAcc;
				}
			}
			Debug.Log ("RotationSpeed: " + rotationSpeed);
			Debug.Log ("AccSpeed: " + rotationAcc);
			RotateLeft();
		} 
		
		// drehung rechts
		if (Input.GetKey (KeyCode.RightArrow)) {
			if(rotationSpeed < rotationSpeedMax){
				rotationSpeed = rotationSpeed * rotationAcc;
				if(rotationAcc < 1.2f){
					rotationAcc = rotationAcc * rotationAcc;
				}
			}
			Debug.Log ("RotationSpeed: " + rotationSpeed);
			RotateRight();
		}
		
		//////////////////////
		// XBOX360 CONTROLLER
		/////////////////////
		
		// linker Stick
		// hoch
		if (Input.GetAxis ("Vertical") > controllerSensitivity) {
			GoUp();
		}
		
		// runter
		if (Input.GetAxis ("Vertical") < -controllerSensitivity) {
			GoDown();
		}
		
		// neigung links
		if (Input.GetAxis ("Horizontal") > controllerSensitivity) {
			GoLeft(Input.GetAxis ("Horizontal2"));
		}
		
		// neigung rechts
		if (Input.GetAxis ("Horizontal") < -controllerSensitivity) {
			GoRight(Input.GetAxis ("Horizontal2"));
		}
		
		// neigung vor
		if (Input.GetAxis ("Vertical2") > controllerSensitivity) {
			GoForward(Input.GetAxis ("Vertical2"));
		}
		
		// neigung hinten
		if (Input.GetAxis ("Vertical2") < -controllerSensitivity) {
			GoBack(Input.GetAxis ("Vertical2"));
		}
		
		// drehung links
		if (Input.GetAxis ("Horizontal2") > controllerSensitivity) {
			RotateLeft();
		}
		
		// drehung rechts
		if (Input.GetAxis ("Horizontal2") < -controllerSensitivity) {
			RotateRight();
		}
		
		// KEINE EINGABE ERKANNT
		if ((!Input.anyKey && !AnyControllerInput()))  {
			ResetRotation();
		}
	}
	
	public bool AnyControllerInput(){
		if ((Input.GetAxis ("Vertical2") < controllerSensitivity && Input.GetAxis ("Vertical2") > -controllerSensitivity) && (Input.GetAxis ("Horizontal2") < controllerSensitivity && Input.GetAxis ("Horizontal2") > -controllerSensitivity)) {
			return false;
		}
		return true;
	}
	
	// drohne steigt
	public void GoUp(){
		transform.Translate(Vector3.up * movementSpeed);
	}
	
	// drohne sinkt
	public void GoDown(){
		transform.Translate(Vector3.down * movementSpeed);
	}
	
	// drohne dreht nach links
	public void RotateLeft(){
		transform.Rotate(Vector3.down * rotationSpeed);
	}
	
	// drohne dreht nach rechts
	public void RotateRight(){
		transform.Rotate(Vector3.up * rotationSpeed);
	}
	
	// drohne fliegt vorwaerts
	public void GoForward(float neigung){
		transform.Translate(Vector3.forward * movementSpeed);
		drone.transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x+15*neigung, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}
	
	// drohne fliegt zurueck
	public void GoBack(float neigung){
		transform.Translate(Vector3.back * movementSpeed);
		drone.transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x+15*neigung, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}
	
	// drohne fliegt nach links
	public void GoLeft(float neigung){
		transform.Translate(Vector3.left * movementSpeed);
		drone.transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+15*neigung);
	}
	
	// drohne fliegt nach rechts
	public void GoRight(float neigung){
		transform.Translate(Vector3.right * movementSpeed);
		drone.transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+15*neigung);
	}
	
	// neigung der drohne zurücksetzen
	public void ResetRotation(){
		drone.transform.rotation = Quaternion.Slerp(drone.transform.rotation, transform.rotation, rotationSpeed); 
	}
}