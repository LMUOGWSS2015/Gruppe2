using UnityEngine;
using System.Collections;

public class LightControl : Photon.MonoBehaviour {

	public Light propRR;
	public Light propRL;
	public Light propFR;
	public Light propFL;
	
	
	// Use this for initialization
	void Start () {
		Debug.Log ("LightControl () - Start");
//		propRR = GameObject.Find("PropRR").GetComponent<Light> ();
//		propRL = GameObject.Find("PropRL").GetComponent<Light> ();
//		propFR = GameObject.Find("PropFR").GetComponent<Light> ();
//		propFL = GameObject.Find("PropFL").GetComponent<Light> ();
//
		shootLight();
	}
	
	// Update is called once per frame
	void Update () {

		/*if (Time.frameCount % 50 <= 25) {
			propRR.intensity = 0.5f;
			propRL.intensity = 0.5f;
			propFR.intensity = 0.5f;
			propFL.intensity = 0.5f;

		} else {
			propRR.intensity = 0.0f;
			propRL.intensity = 0.0f;
			propFR.intensity = 0.0f;
			propFL.intensity = 0.0f;
		}*/

		//StartCoroutine("Fade");

		
	}

	public void shootLight(){
		StartCoroutine("ColorChange");
	}
	

	public IEnumerator ColorChange() {
		for (float f = 1f; f >= 0; f -= 0.05f) {
				propRR.color = Color.Lerp(Color.red, propRR.color, f);
				propRL.color = Color.Lerp(Color.red, propRL.color, f);
				propFR.color = Color.Lerp(Color.red, propFR.color, f);
				propFL.color = Color.Lerp(Color.red, propFL.color, f);

			if(f<=0.05){
				for (f = 1f; f >= 0; f -= 0.1f) {
					propRR.color = Color.Lerp(Color.white, propRR.color, f);
					propRL.color = Color.Lerp(Color.white, propRL.color, f);
					propFR.color = Color.Lerp(Color.white, propFR.color, f);
					propFL.color = Color.Lerp(Color.white, propFL.color, f);
				}

			}
			yield return f;
		}
	}

	IEnumerator Fade() {
		for (float f = 1f; f >= 0; f -= 0.01f) {
			propRR.intensity = f;
			propRL.intensity = f;
			propFR.intensity = f;
			propFL.intensity = f;

			yield return f;
		}
	}


}
