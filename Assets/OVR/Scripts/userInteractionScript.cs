using UnityEngine;
using System.Collections;

public class userInteractionScript : MonoBehaviour {

	public void userInteraction() {
		this.gameObject.renderer.material.SetColor("_Color", Color.cyan);
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
