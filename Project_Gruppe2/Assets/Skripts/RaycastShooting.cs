using UnityEngine;
using System.Collections;
using iView;

public class RaycastShooting : GazeMonobehaviour
{
	
	public Rigidbody projectile;
	public float projectileSpeed = 100;
	public AudioClip fireSound;
	public GameObject Effect;
	public GameObject bulletHole;
	public GameObject explosion;
	public GameObject Fire;
	private int theDamage = 20;
	private AudioSource fireSource;
	float gazeX, gazeY;
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
		Ray ray;
		if (Application.platform == RuntimePlatform.WindowsPlayer) {
			// eye tracking
			gazeX = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ().x;
			gazeY = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ().y;
			// Debug.Log ("GazePosXY: " + gazeX + " , " + gazeY);
			
			if (gazeX == 0 && gazeY == Screen.height) {
				crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
				gazeX = Screen.width / 2;
				gazeY = Screen.height / 2;
				
			} else if (gazeX < 0 || gazeX > Screen.width || gazeY < 0 || gazeY > Screen.height) {
				crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
				gazeX = Screen.width / 2;
				gazeY = Screen.height / 2;
			} else {
				crosshairRect = new Rect (gazeX - crosshairSize / 2, Screen.height - gazeY - crosshairSize / 2, crosshairSize, crosshairSize);
			}
			
			Vector3 gazePosition = new Vector3 (gazeX, gazeY, 0);

			ray = Camera.main.ScreenPointToRay (gazePosition);
		} else {
			ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
			crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);

		}




		

		if (Input.GetButtonDown ("Fire1")) {	
			GameObject go = GameObject.Find ("QuadCopter");
			LightControll other = (LightControll)go.GetComponent (typeof(LightControll));
			other.shootLight ();

			// play fire sound when fire button is pressed
			fireSource.PlayOneShot (fireSound, 1);

			// show bullet
			 

			if (Physics.Raycast (ray, out hit, 100)) {	
				//bullet hole

				//Rigidbody instantiatedProjectile = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
				//instantiatedProjectile.velocity = (hit.point - transform.position).normalized * 300;
				//Destroy (instantiatedProjectile.gameObject, 3);

				//instantiatedProjectile.velocity = (hit.point - transform.position).normalized * 500;
				//instantiatedProjectile.rotation = Quaternion.LookRotation(instantiatedProjectile.velocity);

				//GameObject newBall = Instantiate(ball, transform.position, transform.rotation) as GameObject;
				//newBall.rigidbody.velocity = (hit.point - transform.position).normalized * speed;


				GameObject bulletHoleClone = Instantiate (bulletHole, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
					
				float rand = Random.Range (0.01f, 0.02f);
				bulletHoleClone.transform.localScale = new Vector3 (rand, rand, rand);

				GameObject explosionClone = Instantiate (explosion, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
				Destroy (explosionClone, 5);

				GameObject fireClone = Instantiate (Fire, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
				Destroy (fireClone, 5);

				if (hit.transform.tag == "cube") {
					Debug.Log (hit.transform.tag);
					bulletHoleClone.transform.parent = hit.transform;
					fireClone.transform.parent = hit.transform;
				} else {
					Destroy (bulletHoleClone.gameObject, 15);
				}

				//particle filter effect
				GameObject particleClone = Instantiate (Effect, hit.point, Quaternion.LookRotation (hit.normal)) as GameObject;
				Destroy (particleClone, 2);
				hit.transform.SendMessage ("ApplyDamage", theDamage, SendMessageOptions.DontRequireReceiver);
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
