//using UnityEngine;
//using System.Collections;
//
//public class Crosshair : MonoBehaviour {
//
//	Rect crosshairRect;
//	Texture crosshairTexture;
//
//	// Use this for initialization
//	void Start () {
//		float crosshairSize = Screen.width * 0.1f;
//		crosshairTexture = Resources.Load("crosshair") as Texture;
//		crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
//	}
//
//	void OnGUI(){
//		GUI.DrawTexture (crosshairRect, crosshairTexture);
//	}
//
//	// Update is called once per frame
//	void Update () {
//	
//	}
//}

//using UnityEngine;
//using System.Collections;
//
//public class Crosshair : MonoBehaviour {
//	
//	private Texture crosshairTexture;
//	public Transform crosshair;
//	public Transform endpoint;
//
//	private Camera myCamera;
//	
//	void Start() {
//		crosshairTexture = Resources.Load("crosshair") as Texture;	
//	}
//	
//	
//	void Update() {
//		bool foundHit = false;
//
//		RaycastHit hit = new RaycastHit ();
//
//		foundHit = Physics.Raycast (transform.position, transform.forward, out hit, 20);
//
//		if (foundHit) {
//			crosshair.position = hit.point;
//		} else {
//			crosshair.position = endpoint.position;
//		}
////		Vector3 screenPos = myCamera.WorldToScreenPoint(myTransform.position);
////		screenPos.y = Screen.height - screenPos.y; //The y coordinate on screenPos is inverted so we need to set it straight
//		GUI.DrawTexture(new Rect(screenPos.x, screenPos.y, cursorWidth, cursorHeight), crosshairTexture);
//	}
//}
