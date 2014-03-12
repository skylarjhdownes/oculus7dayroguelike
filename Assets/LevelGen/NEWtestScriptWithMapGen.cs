
using UnityEngine;
using System.Collections;
using MyNameSpace;

public class NEWtestScriptWithMapGen : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {
		Map p = new Map (1000, 1000, 101);

		p.AddRoom (Room_Type.SpawnRoom, 0);
		p.AddRoom (Room_Type.SmallRoom, 1);
		p.AddRoom (Room_Type.MediumRoom, 2);
		p.AddRoom (Room_Type.SmallRoom, 3);
		p.AddRoom (Room_Type.SmallRoom, 3);
		p.AddRoom (Room_Type.SmallRoom, 4);
		p.AddRoom (Room_Type.MediumRoom, 4);

		p.buildMap ();

		d = new dungeonMap (p.width, 1, p.height);
		hallwayBrush h = hallwayBrush.prepare (d);
		terrainRoomBrush g = terrainRoomBrush.prepare (d);
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
