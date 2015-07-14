﻿using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	public static bool isSinglePlayer = true;
	public static bool offlineMode = false;
	public static bool enableEyeTracking = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleEyeTracking(){
		if (enableEyeTracking == false) {
			enableEyeTracking = true;
		} else {
			enableEyeTracking = false;
		}
		Debug.Log ("EYETRACKING: " + enableEyeTracking);
	}

	public GameObject CustomInstantiate (string gameObjectString, RaycastHit myHit)
	{
		GameObject myGameObject = null;
		if (isSinglePlayer) {
			GameObject myResource = (GameObject)Resources.Load (gameObjectString, typeof(GameObject));
			myGameObject = (GameObject)Instantiate (myResource, myHit.point, Quaternion.LookRotation (Vector3.up, myHit.normal));
		} else {
			myGameObject = (GameObject)PhotonNetwork.Instantiate (gameObjectString, myHit.point, Quaternion.LookRotation (Vector3.up, myHit.normal), 0);
		}
		return myGameObject;
	}
}
