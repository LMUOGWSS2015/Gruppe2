using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {

	public void BackToMainMenuClick (int lvl) {
		Application.LoadLevel (lvl);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
