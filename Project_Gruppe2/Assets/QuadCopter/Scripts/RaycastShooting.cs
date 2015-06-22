using UnityEngine;
using System.Collections;

public class RaycastShooting : MonoBehaviour
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
		float crosshairSize = Screen.width * 0.05f;
		crosshairTexture = Resources.Load ("crosshair") as Texture;
		crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);

	}
	
	// Update is called once per frame
	void Update ()
	{
		// show particle filter effect + show bullet hole
		RaycastHit hit;
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);


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
				GameObject bulletHoleClone = Instantiate (bulletHole, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
					
				float rand = Random.Range (0.01f, 0.02f);
				bulletHoleClone.transform.localScale = new Vector3 (rand, rand, rand);

				GameObject explosionClone = Instantiate (explosion, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
				Destroy (explosionClone, 5);

				GameObject fireClone = Instantiate (Fire, hit.point, Quaternion.LookRotation (Vector3.up, hit.normal)) as GameObject;
				Destroy (fireClone, 5);

				if(hit.transform.tag == "cube"){
					Debug.Log (hit.transform.tag);
					bulletHoleClone.transform.parent = GameObject.Find ("Enemy").transform;
				}else{
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
