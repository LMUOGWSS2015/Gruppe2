using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	private bool isSinglePlayer = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool GetIsSinglePlayer(){
		return this.isSinglePlayer;
	}

	public void SetIsSinglePlayer(bool isSinglePlayer){
		this.isSinglePlayer = isSinglePlayer;
	}

	public GameObject CustomInstantiate (string gameObjectString, RaycastHit myHit)
	{
		GameObject myGameObject = null;
		if (this.isSinglePlayer) {
			GameObject myResource = (GameObject)Resources.Load (gameObjectString, typeof(GameObject));
			myGameObject = (GameObject)Instantiate (myResource, myHit.point, Quaternion.LookRotation (Vector3.up, myHit.normal));
		} else {
			myGameObject = (GameObject)PhotonNetwork.Instantiate (gameObjectString, myHit.point, Quaternion.LookRotation (Vector3.up, myHit.normal), 0);
		}
		return myGameObject;
	}
}
