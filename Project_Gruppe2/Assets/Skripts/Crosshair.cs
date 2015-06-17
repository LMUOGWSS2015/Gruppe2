using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	Rect crosshairRect;
	Texture crosshairTexture;

	// Use this for initialization
	void Start () {
		float crosshairSize = Screen.width * 0.1f;
		crosshairTexture = Resources.Load("crosshair") as Texture;
		crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
	}

	void OnGUI(){
		GUI.DrawTexture (crosshairRect, crosshairTexture);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
