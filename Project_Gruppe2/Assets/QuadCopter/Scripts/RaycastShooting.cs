using UnityEngine;
using System.Collections;
public class RaycastShooting : MonoBehaviour {
	
	public Rigidbody projectile;
	public float projectileSpeed = 100;
	public AudioClip fireSound;
	public Transform Effect;
	public GameObject bulletHole;
	public GameObject crosshair;

	private int theDamage = 20;
	private AudioSource fireSource;
	
	// Use this for initialization
	void Start () {
		fireSource = GetComponent<AudioSource>();
		crosshair = Instantiate(crosshair, new Vector3(0,0,0), Quaternion.LookRotation(Vector3.up, new Vector3(0,0,0))) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		// show particle filter effect + show bullet hole
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast (ray, out hit, 20)) {	
			Debug.Log ("#1" + hit.point);
			crosshair.transform.position = hit.point;
			crosshair.transform.rotation = Quaternion.LookRotation (Vector3.up, hit.normal);
//			float crosshairSize = Screen.width * 0.1f;
//			Rect crosshairRect = new Rect (Screen.width / 2 - crosshairSize / 2, Screen.height / 2 - crosshairSize / 2, crosshairSize, crosshairSize);
//			GUI.DrawTexture (crosshairRect, crosshairTexture);
		} else {
			Debug.Log ("endpoint reached!");
			Vector3 endpoint = ray.origin + (ray.direction * 20);
			crosshair.transform.position = endpoint;
		}

		if (Input.GetButtonDown("Fire1"))
		{	
			GameObject go = GameObject.Find("QuadCopter");
			LightControll other = (LightControll) go.GetComponent(typeof(LightControll));
			other.shootLight();

			// play fire sound when fire button is pressed
			fireSource.PlayOneShot(fireSound, 1);

			// show bullet
			Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, projectileSpeed));
			Destroy(instantiatedProjectile.gameObject, 3); 

			if(Physics.Raycast(ray,out hit,100))
				{	
					//bullet hole
					bulletHole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(Vector3.up, hit.normal)) as GameObject;
					
					float rand = Random.Range (0.01f,0.02f);
					bulletHole.transform.localScale = new Vector3(rand,rand,rand);
					bulletHole.transform.parent = GameObject.Find ("Enemy").transform;

					//particle filter effect
					GameObject particleClone = Instantiate(Effect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
					hit.transform.SendMessage("ApplyDamage", theDamage, SendMessageOptions.DontRequireReceiver);
					Destroy(particleClone, 2);
				}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Target")
		{
			Destroy(col.gameObject);
		}
	}

}
