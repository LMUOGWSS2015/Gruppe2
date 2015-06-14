using UnityEngine;
using System.Collections;
public class ShootDemo : MonoBehaviour {
	
	public Rigidbody projectile;
	public float speed = 100;
	public AudioClip fireSound;

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

			Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
			
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
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
