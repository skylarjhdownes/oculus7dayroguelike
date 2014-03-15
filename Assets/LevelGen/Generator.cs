
using UnityEngine;
using System.Collections.Generic;
using MyNameSpace;

namespace LevelGen {
	public class Generator : MonoBehaviour {
		private Dungeon d;
		System.Random rng;
		public int level = 0;
		private List<List<Room>> levels =new List<List<Room>> ();

		// Use this for initialization
		void Start() {
			DontDestroyOnLoad (this.gameObject);
			RenderSettings.ambientLight = Color.black;

			var oldgen = GameObject.FindGameObjectWithTag ("Respawn");
			if (oldgen != null) {
				var oldscript = oldgen.GetComponent<Generator>();
								level = oldscript.level + 1;
								rng = oldscript.rng;
								Destroy(oldgen);
						} else {
								rng = new System.Random ();
						}

			gameObject.tag = "Respawn";

			// level 0
			levels.Add (new List<Room> {
						new SpawnRoom(rng),
						new MediumRoom(rng, 1),
						new FinalRoom(rng, 2)
			});
			// level 1
			levels.Add (new List<Room> {
						new SpawnRoom(rng),
						new MediumRoom(rng, 1),
						new MediumRoom(rng, 2),
						new MediumRoom(rng, 2),
						new MediumRoom(rng, 2),
						new FinalRoom(rng, 3)
			});
			//level 2
			/*
			var l3 = new List<Room> { new SpawnRoom(rng) };
			for (int i = 1; i < 12; i++)
								for (int k = 0; k < Mathf.Pow(i, 1.618f); k++)
										l3.Add (new SmallRoom (rng, i));
			levels.Add (l3);*/

			DrawLevel ();
		}

		public void DrawLevel() {

			d = new Dungeon ();

			var p = new Map (d, rng, levels[level]);

			p.buildMap ();
			
			d.RenderAll ();

			GameObject.FindGameObjectWithTag ("Player").transform.position = d.Scaled(new Vector3(2f, 20f, 2f));
		}

		// Update is called once per frame
		void Update () {
			
		}
	}
}
