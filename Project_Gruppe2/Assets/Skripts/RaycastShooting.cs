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
	private Utils utils;
	private bool isSinglePlayer;

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
		utils = GameObject.Find ("_GLOBAL_SCRIPTS").GetComponent<Utils> ();
		isSinglePlayer = Utils.isSinglePlayer;
	}

	// Update is called once per frame
	void Update ()
	{	
		if(Utils.enableEyeTracking){
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
		}
		else{
			crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
			gazeX = Screen.width / 2;
			gazeY = Screen.height / 2;
			Vector3 gazePosition = new Vector3 (gazeX, gazeY, 0);
			ray = Camera.main.ScreenPointToRay (gazePosition);
		}


	
		// player shoots
		if (Input.GetButtonDown ("Fire1")) {
//			int layerMask = 1 << 8;
//			
//			// Does the ray intersect any objects which are in the player layer.
//			if (Physics.Raycast(transform.position, Vector3.forward, Mathf.Infinity, layerMask))
//				Debug.Log("The ray hit the player");

			// show light
			GameObject.Find ("QuadCopter").GetComponent<LightControl> ().shootLight ();

			// play fire sound when fire button is pressed
			fireSource.PlayOneShot (fireSound, 1);
//			if(Physics.SphereCast(ray, 2f, out hit, 150)){
			if (Physics.Raycast (ray, out hit, 150)) {
				// bullet hole
				GameObject bulletHoleClone = utils.CustomInstantiate ("BulletHole", hit);

				float rand = Random.Range (0.01f, 0.02f);
				bulletHoleClone.transform.localScale = new Vector3 (rand, rand, rand);

				// explosion effect
				GameObject explosionClone = utils.CustomInstantiate ("ExplosionMobile", hit);
				Destroy (explosionClone, 5);

				// fire around bullet holes

				GameObject fireClone = utils.CustomInstantiate ("FireMobile", hit);
				Destroy (fireClone, 5);

				// if we hit a cube or a player, add bullet hole and fire clone to cube object
				if (hit.transform.tag == "cube" || hit.transform.tag == "Player") {
					bulletHoleClone.transform.parent = hit.transform;
					fireClone.transform.parent = hit.transform;
				} 
				// if we hit something else, destroy bullethole after 15 seconds
				else {
					Destroy (bulletHoleClone.gameObject, 15);
				}

				// particle filter effect
				GameObject particleClone = utils.CustomInstantiate ("Particle System", hit);
				Destroy (particleClone, 2);
				string myTag = Utils.isSinglePlayer ? "cube":"Player";
				if (hit.transform.tag == myTag) {
					if (isSinglePlayer) {
						hit.transform.SendMessage ("ApplyDamage2", theDamage, SendMessageOptions.DontRequireReceiver);
					} else {
						if (hit.transform.GetComponent<EnemyHealth> ().GetComponent<PhotonView> () == null) {
							Debug.LogError ("Photon View not available!");
						} else {
							PhotonPlayer hitPlayer = PhotonPlayer.Find(hit.transform.GetComponent<PhotonView>().ownerId);
							hit.transform.GetComponent<EnemyHealth> ().GetComponent<PhotonView> ().RPC ("ApplyDamage", PhotonTargets.All, theDamage, PhotonNetwork.player, hitPlayer);
						}
					}
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
