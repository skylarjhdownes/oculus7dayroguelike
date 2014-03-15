
using UnityEngine;
using System.Collections.Generic;
using MyNameSpace;

namespace LevelGen {
	public class Generator : MonoBehaviour {
		private Dungeon d;
		public int seed = 10;
		private System.Random rng;
		public int level = 0;
		private List<List<Room>> levels =new List<List<Room>> ();

		// Use this for initialization
		void Start() {
			RenderSettings.ambientLight = Color.black;
			rng = new System.Random (seed);

			// level 0
			levels.Add (new List<Room> {
						new SpawnRoom(rng),
						new MediumRoom(rng, 1)
			});
			// leve 1
			levels.Add (new List<Room> {
						new SpawnRoom(rng),
						new MediumRoom(rng, 1),
						new MediumRoom(rng, 2),
						new MediumRoom(rng, 2),
						new MediumRoom(rng, 2)
			});

			DrawLevel ();
		}

		public void DrawLevel() {
			d = new Dungeon ();

			var p = new Map (d, rng, levels[level]);

			p.buildMap ();
			
			d.RenderAll ();
		}

		// Update is called once per frame
		void Update () {
			
		}
	}
}
