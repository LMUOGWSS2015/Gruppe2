using UnityEngine;
using System.Collections;

public class Menu2 : MonoBehaviour {
	

	// Update is called once per frame
	public void ChangeToScene (int sceneToChangeTo) {
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
