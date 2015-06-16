using UnityEngine;
using System.Collections;
public class ShootDemo : MonoBehaviour {
	
	public Rigidbody projectile;
	public float speed = 100;
	public AudioClip fireSound;
	public Transform Effect;
	public GameObject bulletHole;


	private AudioSource fireSource;
	
	// Use this for initialization
	void Start () {
		fireSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Fire1"))
		{	
			// play fire sound when fire button is pressed
			fireSource.PlayOneShot(fireSound, 1);

			// show bullet
			Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));

			RaycastHit hit;
			Ray ray = new Ray(transform.position, transform.forward);
				if(Physics.Raycast(ray,out hit,100))
				{		
				    Instantiate(bulletHole, hit.point, Quaternion.LookRotation(Vector3.up, hit.normal));
					float rand = Random.Range (0.01f,0.02f);
					bulletHole.transform.localScale = new Vector3(rand,rand,rand);
					GameObject particleClone = Instantiate(Effect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
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
