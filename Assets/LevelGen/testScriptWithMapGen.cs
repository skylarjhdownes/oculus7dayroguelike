
using UnityEngine;
using System.Collections;
using MyNameSpace;

public class testScriptWithMapGen : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {
		Map p = null; //MapGenerator.generateMapWithRectangularRoomsFirst (60, 60, .15, 251);

		d = new dungeonMap (p.width, 1, p.height);
		hallwayBrush h = hallwayBrush.prepare (d);

		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p.MapGrid[i,j]) {
				case 0:
				case 2:
					d.place (i,0,j, h);
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
