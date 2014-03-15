using UnityEngine;
using System.Collections;

public  class userInteractionScript : MonoBehaviour {
	private bool moving = false;
	private int distanceToMove = 30;
	public AudioClip grindSound;

	public void userInteraction() {
		moving = true;
		//this.gameObject.transform.renderer.material.SetColor("_Color", Color.cyan);
		
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (moving == true && distanceToMove == 30) {
			audio.PlayOneShot(grindSound);
		}
		if (moving == true && distanceToMove > 0) {
			distanceToMove--;
			this.gameObject.transform.Translate(0, Time.deltaTime * 1, 0);
		}
	}
}
