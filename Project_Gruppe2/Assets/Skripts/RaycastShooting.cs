using UnityEngine;
using System.Collections;
using iView;

public class RaycastShooting : GazeMonobehaviour
{
	
	public Rigidbody projectile;
	public float projectileSpeed = 100;
	public AudioClip fireSound;
	public GameObject Effect;
	public GameObject explosion;
	public GameObject Fire;

	private int theDamage = 20;
	private AudioSource fireSource;
	private float gazeX, gazeY;
	private float crosshairSize;
	private Rect crosshairRect;
	private Texture crosshairTexture;
	private RaycastHit hit;
	private Ray ray;
	private EnemyHealth enemyHealth;

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
		enemyHealth = this.transform.GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update ()
	{	
		// only use eye tracker on windows -> TODO: should only be used when eye tracker is connected
		if (Application.platform == RuntimePlatform.WindowsPlayer) {

			// eye tracking
			gazeX = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ().x;
			gazeY = iView.SMIGazeController.Instance.GetSample ().averagedEye.gazePosInUnityScreenCoords ().y;
			
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

		// player shoots
		if (Input.GetButtonDown ("Fire1")) {
			// show light
			GameObject.Find ("QuadCopter").GetComponent<LightControl>().shootLight();

			// play fire sound when fire button is pressed
			fireSource.PlayOneShot (fireSound, 1);

			if (Physics.Raycast (ray, out hit, 100)) {
				//Rigidbody instantiatedProjectile = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
				//instantiatedProjectile.velocity = (hit.point - transform.position).normalized * 300;
				//Destroy (instantiatedProjectile.gameObject, 3);

				//instantiatedProjectile.velocity = (hit.point - transform.position).normalized * 500;
				//instantiatedProjectile.rotation = Quaternion.LookRotation(instantiatedProjectile.velocity);

				//GameObject newBall = Instantiate(ball, transform.position, transform.rotation) as GameObject;
				//newBall.rigidbody.velocity = (hit.point - transform.position).normalized * speed;

				// bullet hole
				GameObject bulletHoleClone = (GameObject) PhotonNetwork.Instantiate("BulletHole", hit.point, Quaternion.LookRotation (Vector3.up, hit.normal), 0);
				float rand = Random.Range (0.01f, 0.02f);
				bulletHoleClone.transform.localScale = new Vector3 (rand, rand, rand);

				// explosion effect
				GameObject explosionClone = (GameObject) PhotonNetwork.Instantiate ("ExplosionMobile", hit.point, Quaternion.LookRotation (Vector3.up, hit.normal), 0);
				Destroy (explosionClone, 5);

				// fire around bullet holes
				GameObject fireClone = (GameObject) PhotonNetwork.Instantiate ("FireMobile", hit.point, Quaternion.LookRotation (Vector3.up, hit.normal), 0);
				Destroy (fireClone, 5);

				if (hit.transform.tag == "cube") {
					bulletHoleClone.transform.parent = hit.transform;
					fireClone.transform.parent = hit.transform;
				} else {
					Destroy (bulletHoleClone.gameObject, 15);
				}

				// particle filter effect
				GameObject particleClone = (GameObject) PhotonNetwork.Instantiate ("Particle System", hit.point, Quaternion.LookRotation (hit.normal), 0);
				Destroy (particleClone, 2);

				if(hit.transform.tag == "Player"){
					enemyHealth.ApplyDamage(theDamage);
					hit.transform.GetComponent<PhotonView>().RPC("ApplyDamage", PhotonTargets.All, theDamage);
					//hit.transform.SendMessage ("ApplyDamage", theDamage, SendMessageOptions.DontRequireReceiver);
				}
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
