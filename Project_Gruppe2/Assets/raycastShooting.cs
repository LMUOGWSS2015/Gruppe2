using UnityEngine;
using System.Collections;
public class RaycastShooting : MonoBehaviour
{
	public int theDamage;
	
	void Update ()
	{
		RaycastHit hit;
		
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2,0));
		
		if(Input.GetButtonDown("Fire1"))
		{
			if(Physics.Raycast(ray,out hit,100))
			{
				
				Debug.Log ("Working");
			}
		}
	}
}