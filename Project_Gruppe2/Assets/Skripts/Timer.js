#pragma strict

var timer : float;
//var counter : Rect = new Rect(Screen.width/2-300, Screen.height/2-150, 100, 100);
	
var font : Font;
var myStyle : GUIStyle; 
myStyle.normal.textColor = Color.white;
myStyle.alignment = TextAnchor.MiddleCenter;
myStyle.fontSize = 200;
myStyle.font = font;
//myStyle.font = font;	
 	
 	
function Start() {
	timer = 5.0;
} 	
 	
function Update() {
if(timer > 0.0){
timer -= Time.deltaTime;
}


}

function OnGUI() {

	
	if(timer>0.0){
	transform.Translate(Vector3.up/10);
	GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-100, 100, 100), "" + timer.ToString("0"), myStyle);
	
}
}