#pragma strict

var Step : float; //A variable we increment
         
function Start () {

}
         
function FixedUpdate () {
    Step += 0.025;

    //Make sure Steps value never gets too out of hand 
       if(Step > 999999){
           Step = 1;
       }

    //Float up and down along the y axis, 
    transform.position.y = Mathf.Sin(Step)/2;
 }
 

function Update () {

}
