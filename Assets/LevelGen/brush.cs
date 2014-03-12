using UnityEngine;
using System.Collections.Generic;

public abstract class brush : MonoBehaviour {

	abstract public void RenderRoom(Vector3 position, Vector3 size);

	// Children ought to have a public static method called "prepare(dungeonMap map)"
	
	// These have to be here since we're subclassing MonoBehaviour
	void Start() {
	}
	void Update () {
	}
}