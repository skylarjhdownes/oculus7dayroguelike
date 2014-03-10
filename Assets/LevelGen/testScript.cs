using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {
		d = new dungeonMap (3, 2, 2);
		room h = hallways.prepare (d);
		d.place (0, 0, 1, h);
		d.place (1, 0, 0, h);
		d.place (1, 1, 0, h);
		d.place (1, 1, 1, h);
		d.place (0, 1, 0, h);
		d.place (0, 1, 1, h);
		d.RenderAll ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
