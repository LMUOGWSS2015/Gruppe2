using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour
{
	Vector3 realPosition;
	Quaternion realRotation = Quaternion.identity;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (photonView.isMine) {
			// Do nothing -> the character motor/ input etc is moving us
		} else {
			Debug.Log("is not mine");
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			// This is our player. Send actual position to the network
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			// This is someone else player. Receive their position (as of a few ms ago) and update our position of the player
			transform.position = (Vector3) stream.ReceiveNext();
			transform.rotation = (Quaternion) stream.ReceiveNext();
		}
	}
}
