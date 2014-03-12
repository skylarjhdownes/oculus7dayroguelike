using UnityEngine;
using System.Collections.Generic;

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
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log("Clicked left mouse button.");
				if (hitInfo.collider.gameObject.GetComponent("userInteractionScript")!=null){
					hitInfo.collider.gameObject.GetComponent("userInteractionScript").SendMessage("userInteraction");
					Debug.Log (hitInfo.collider.gameObject);
				}
			}
			Debug.DrawRay(transform.position, transform.forward, Color.white, 6.0f);
//			if (hitInfo.collider.interactable 
		}
	}
}
