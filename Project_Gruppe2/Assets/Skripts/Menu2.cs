using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {

	// Update is called once per frame
	public void ChangeToScene (int sceneToChangeTo) {
		switch (sceneToChangeTo)
		{
		case 0:
			Utils.enableEyeTracking = false;
			break;
		case 1:
			Utils.isSinglePlayer = true;
			break;
		case 2:
			Utils.isSinglePlayer = true;
			break;
		case 3:
			Utils.isSinglePlayer = true;
			break;
		case 4: 
			Utils.isSinglePlayer = true;
			break;
		case 5: 
			Utils.isSinglePlayer = true;
			break;
		case 6: 
			Utils.isSinglePlayer = true;
			break;
		case 7: 
			Utils.isSinglePlayer = false;
			break;
		default:
			Debug.Log("This should not happen!");
			break;
		}
		Application.LoadLevel (sceneToChangeTo);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	/* TODO muss noch getestet werden
	void Update(){ 
	//quit game if escape key is pressed
	if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
	}
	*/
}
