using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class testScript : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {
		d = new dungeonMap (2, 2, 2);
		room e = null;
		hallways h = hallways.prepare (d);
		d.map = new room[2,2,2] {{{h, e},{h, h}}, {{e, h},{h, h}}};
		d.RenderAll ();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
