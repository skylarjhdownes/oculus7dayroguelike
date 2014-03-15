using UnityEngine;
using System.Collections;

namespace LevelGen {
	public class ChandelierBrush : Brush {
		private GameObject c = (GameObject)Resources.Load("Chandelier");

		System.Random rng = new System.Random();
		
		public ChandelierBrush() {
			 
		}

		public void Render (Position pos, Dungeon map)
		{
			if (rng.Next (20) == 0) {
								var end = (GameObject)Object.Instantiate (c);
								end.transform.position = map.Scaled (pos.Vector3 + new Vector3 (0f, 0.3f, 0f));
						}
		}
	}
}