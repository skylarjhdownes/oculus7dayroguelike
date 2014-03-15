using UnityEngine;
using System.Collections;

namespace LevelGen {
	public class EndBrush : Brush {
		private GameObject endTrigger = (GameObject)Resources.Load("exit2");
		private Dungeon map;
		
		public EndBrush() {
			 
		}

		public void Render (Position pos, Dungeon map)
		{
			var end = (GameObject)Object.Instantiate(endTrigger);
			end.transform.position = pos.Vector3;
			map.AddChild (end);
		}
	}
}