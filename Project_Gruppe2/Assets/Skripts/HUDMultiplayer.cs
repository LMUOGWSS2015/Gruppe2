using UnityEngine;
using System.Collections;

public class HUDMultiplayer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnGUI(){
		DrawHealthPoints ();
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void DrawHealthPoints ()
	{
		float rectWidth = 150;
		float rectHeight = 30;

		GUIStyle myStyle = new GUIStyle();
		myStyle.font = (Font)Resources.Load("Fonts/ethnocentric rg.ttf");
		myStyle.normal.textColor = Color.white;
	
		Rect rect = new Rect (Screen.width - 160, Screen.height - 35, rectWidth, rectHeight);
		GUILayout.BeginArea (rect, new GUIStyle ("box"));
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("HEALTH POINTS:", myStyle);
		GUILayout.Label (EnemyHealth.health.ToString(), myStyle);
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
	
	
}