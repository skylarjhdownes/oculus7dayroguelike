
using UnityEngine;
using System.Collections.Generic;
using MyNameSpace;

namespace LevelGen {
public class GeneratorM : MonoBehaviour {
	private Dungeon d;
	public int seed = 10;

	// Use this for initialization
	void Start() {
		
		var rng = new System.Random (seed);
			
		d = new Dungeon ();
		var p = new Map (d, rng);

		p.AddRoom (Room_Type.SpawnRoom, 0);
		p.AddRoom (Room_Type.SmallRoom, 1);
		p.AddRoom (Room_Type.LargeRoom, 2);
		p.AddRoom (Room_Type.SmallRoom, 3);
		p.AddRoom (Room_Type.SmallRoom, 3);
		p.AddRoom (Room_Type.MediumRoom, 3);
		p.AddRoom (Room_Type.SmallRoom, 4);

		p.buildMap ();

		var hallwayTorches = new torchBrush();

		for (int i = 0; i < 100; i++) {
			for (int j = 0; j < 100; j++) {
				switch (p[i,j]) {
				case 0:
				case 2:
					//d.place (i,0,j, h);
					d.Place (new Position(i,0,j), hallwayTorches);
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
}
