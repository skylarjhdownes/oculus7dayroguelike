using UnityEngine;
using System.Collections;

public class OVRInteraction : MonoBehaviour {

	public float pickupDistance = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, transform.forward, out hitInfo, pickupDistance)) {
			Debug.Log("Looking at: " + hitInfo.collider);
			Debug.DrawRay(transform.position, transform.forward, Color.white, 6.0f);
		}
	}
}
