using UnityEngine;
using System.Collections;

//  Make sure that the collider this is attached to is a trigger.  (Checkbox)

public class enterNextLevel : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Application.LoadLevel ("main");
	}
}