using UnityEngine;
using System.Collections;
public class ShootDemo : MonoBehaviour {
	
	public Rigidbody projectile;
	public float speed = 100;
	public AudioClip fireSound;
	public Transform Effect;


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

			RaycastHit hit;
			
			Ray ray = new Ray(transform.position, Vector3.forward);

			
				if(Physics.Raycast(ray,out hit,100))
				{		
					Debug.Log ("RayCast");
					//string s = hit.point.ToString();
					//Debug.DrawLine (new Vector3(5,5,5), new Vector3(0,0,0));
					Debug.Log (hit.point);

					GameObject particleClone = new GameObject();
					particleClone = Instantiate(Effect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
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
