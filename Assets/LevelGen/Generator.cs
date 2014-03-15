
using UnityEngine;
using System.Collections.Generic;
using MyNameSpace;

namespace LevelGen {
public class Generator : MonoBehaviour {
	private Dungeon d;
	public int seed = 10;

	// Use this for initialization
	void Start() {

		var rng = new System.Random (seed);
		var p = new Map (rng);

		p.AddRoom (Room_Type.SpawnRoom, 0);
		p.AddRoom (Room_Type.SmallRoom, 1);
		p.AddRoom (Room_Type.LargeRoom, 2);
		p.AddRoom (Room_Type.SmallRoom, 3);
		p.AddRoom (Room_Type.SmallRoom, 3);
		p.AddRoom (Room_Type.MediumRoom, 3);
		p.AddRoom (Room_Type.SmallRoom, 4);

		p.buildMap ();

		d = new Dungeon ();
		var factory = new RoomBrushFactory ();
		var brushes = new List<Brush> ();
		brushes.Add(factory.createRoomBrush (rng));
		brushes.Add(factory.createRoomBrush (rng));
		brushes.Add(factory.createRoomBrush (rng));
		brushes.Add(factory.createRoomBrush (rng));
		var hallwayTorches = new hallwayTorchesBothWallsBrush();

		for (int i = -1000; i < 1000; i++) {
			for (int j = -1000; j < 1000; j++) {
				switch (p[i,j]) {
				case 0:
				case 2:
				//case 98:
						//d.place (i,0,j, h);
					d.Place (new Position(j,0,i), brushes[rng.Next(brushes.Count)]);
					if (rng.Next(20) == 0)
						{
							d.Place (new Position(j,0,i), hallwayTorches);
						}
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
