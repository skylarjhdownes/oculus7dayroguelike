
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
				levels = oldscript.levels;
				rng = oldscript.rng;

				// Check to see if the next level should be created
				Debug.Log ("Level: "+level);
//				if ( oldscript.levels[level] == null ) {
				if ( levels.Count <= level ) {

					// Create the next level
					List<Room> rList = new List<Room>();

					// Iterate over the rooms in the previous level.
					foreach( Room n in oldscript.levels[level-1] ) {

						if ( n.level < level ) rList.Add(n.newInstance (rng,n.level));

						if ( n.level == level-2 ) rList.Add(n.newInstance (rng,level));
						if ( n.level == level-1 ) rList.Add(n.newInstance (rng,level+1));
						if ( n.level == level ) rList.Add(n.newInstance (rng,level));
						if ( n.level == level+1 ) {
							rList.Add(n.newInstance (rng,level+1));
							rList.Add(n.newInstance (rng,level+2));
						}
						if ( n.level == 199 ) rList.Add(n.newInstance (rng,199));

					}

					//rList.Sort();
					//foreach ( Room n in rList ) Debug.Log ("RoomType: "+n.GetType()+"   L: "+n.level);

					levels.Add (rList);
				}

				Destroy(oldgen);
			} else {
				// Create the first Generator script.
				rng = new System.Random ();

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
				
				// level 2
				levels.Add (new List<Room> {
					new SpawnRoom(rng),
					new SmallRoom(rng, 1),
					new MediumRoom(rng, 2),
					new SmallRoom(rng, 2),
					new LargeRoom(rng, 3),
					new SmallRoom(rng, 3),
					new LargeRoom(rng, 4),
					new FinalRoom(rng, 199)
				});
				
				//level 2
				/*
			var l3 = new List<Room> { new SpawnRoom(rng) };
			for (int i = 1; i < 12; i++)
								for (int k = 0; k < Mathf.Pow(i, 1.618f); k++)
										l3.Add (new SmallRoom (rng, i));
			levels.Add (l3);*/

			
			}

			gameObject.tag = "Respawn";


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
