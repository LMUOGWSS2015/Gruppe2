using UnityEngine;
using System.Collections;
using iView;

public class RaycastShooting : GazeMonobehaviour
{
	
	public Rigidbody projectile;
	public float projectileSpeed = 100;
	public AudioClip fireSound;
	public Transform Effect;
	public GameObject bulletHole;
	private int theDamage = 20;
	private AudioSource fireSource;

	float gazeX,gazeY;
	float crosshairSize;
	Rect crosshairRect;
	Texture crosshairTexture;

	void OnGUI ()
	{
		GUI.DrawTexture (crosshairRect, crosshairTexture);
	}
	
	// Use this for initialization
	void Start ()
	{
		fireSource = GetComponent<AudioSource> ();
		crosshairSize = Screen.width * 0.05f;
		crosshairTexture = Resources.Load ("crosshair") as Texture;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// show particle filter effect + show bullet hole
		RaycastHit hit;

		// eye tracking
		gazeX = iView.SMIGazeController.Instance.GetSample().averagedEye.gazePosInUnityScreenCoords().x;
		gazeY = iView.SMIGazeController.Instance.GetSample().averagedEye.gazePosInUnityScreenCoords().y;
		// Debug.Log ("GazePosXY: " + gazeX + " , " + gazeY);

		if(gazeX == 0 && gazeY == Screen.height){
			crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
		}
		else if(gazeX < 0 || gazeX > Screen.width || gazeY < 0 || gazeY > Screen.height){
			crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
		}
		else{
			crosshairRect = new Rect (gazeX - crosshairSize / 2, Screen.height - gazeY - crosshairSize / 2, crosshairSize, crosshairSize);
		}

		Vector3 gazePosition = new Vector3 (gazeX, gazeY, -1.1f);
		
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

		
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay(gazePosition, forward, Color.green);

		Debug.Log ("Gaze Position:" + gazePosition);
		

		if (Input.GetButtonDown ("Fire1")) {	
			GameObject go = GameObject.Find ("QuadCopter");
			LightControll other = (LightControll)go.GetComponent (typeof(LightControll));
			other.shootLight ();

			// play fire sound when fire button is pressed
			fireSource.PlayOneShot (fireSound, 1);

			// show bullet
			//Rigidbody instantiatedProjectile = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
			//instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, projectileSpeed));
			//Destroy (instantiatedProjectile.gameObject, 3); 

			if (Physics.Raycast (ray, out hit, 100)) {	
				//bullet hole
				bulletHole = Instantiate (bulletHole, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
					
				float rand = Random.Range (0.01f, 0.02f);
				bulletHole.transform.localScale = new Vector3 (rand, rand, rand);
				bulletHole.transform.parent = GameObject.Find ("Enemy").transform;

				//particle filter effect
				GameObject particleClone = Instantiate (Effect, hit.point, Quaternion.LookRotation (hit.normal)) as GameObject;
				hit.transform.SendMessage ("ApplyDamage", theDamage, SendMessageOptions.DontRequireReceiver);
				Destroy (particleClone, 2);
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Target") {
			Destroy (col.gameObject);
		}
	}

}
