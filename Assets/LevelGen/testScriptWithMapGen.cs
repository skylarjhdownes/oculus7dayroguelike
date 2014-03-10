
using UnityEngine;
using System.Collections;
using MyNameSpace;

public class testScriptWithMapGen : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {
		Map p = MapGenerator.generateMapWithRectangularRoomsFirst (60, 60, .15, 251);

		d = new dungeonMap (p.width, 1, p.height);
		room e = null;
		hallways h = hallways.prepare (d);
		d.map = new room[p.width, 1, p.height];


		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p.MapGrid[i,j]) {
				case 0:
				case 2:
					d.map[i,0,j] = h;
					break;

				case 99:
					d.map[i,0,j] = e;
					break;

				}
			}
		}

		d.RenderAll ();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
