
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
			
		d = new Dungeon ();
		var p = new Map (d, rng);
			
			p.AddRoom (Room_Type.SpawnRoom, 0);
			p.AddRoom (Room_Type.SmallRoom, 1);
			p.AddRoom (Room_Type.LargeRoom, 2);
			p.AddRoom (Room_Type.SmallRoom, 2);
			p.AddRoom (Room_Type.SmallRoom, 3);
			p.AddRoom (Room_Type.SmallRoom, 3);
			p.AddRoom (Room_Type.MediumRoom, 3);
			p.AddRoom (Room_Type.SmallRoom, 4);
			p.AddRoom (Room_Type.MediumRoom, 4);
			p.AddRoom (Room_Type.SmallRoom, 4);
			p.AddRoom (Room_Type.MediumRoom, 4);
			p.AddRoom (Room_Type.SmallRoom, 4);
			p.AddRoom (Room_Type.MediumRoom, 4);
			p.AddRoom (Room_Type.SmallRoom, 4);
			p.AddRoom (Room_Type.MediumRoom, 4);
			p.AddRoom (Room_Type.LargeRoom, 5);
			p.AddRoom (Room_Type.LargeRoom, 5);
			p.AddRoom (Room_Type.LargeRoom, 5);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);
			p.AddRoom (Room_Type.SmallRoom, 6);

		p.buildMap ();

		d.RenderAll ();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
}
