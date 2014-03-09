using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	public Texture wallTexture;

	// Use this for initialization
	void Start() {
		dungeonMap d = new dungeonMap (3, 2, 3);
		hallways h = new hallways (wallTexture);
		room e = null;
		d.map = new room[2,2,2] {{{h, e},{h, h}}, {{e, h},{h, h}}};
		d.RenderAll ();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
