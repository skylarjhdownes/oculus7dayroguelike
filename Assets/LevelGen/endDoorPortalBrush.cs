using UnityEngine;
using System.Collections;


namespace LevelGen {
	public class endDoorPortalBrush : Brush {
		private GameObject self;
		private GameObject clonableRoomPortal = (GameObject)Resources.Load("exit1");
		private Dungeon map;
		GameObject torch;
		
		public endDoorPortalBrush() {
			
		}
		public void Render (Position pos, Dungeon map)
		{
			GameObject doorPortal = (GameObject)Object.Instantiate(clonableRoomPortal);
			// walls
			if (!map.HasContent(pos + new Position (-1, 0, 0))) {
				doorPortal.transform.Rotate(0,180,0);
				doorPortal.transform.position = map.Scaled(pos.Vector3 + new Vector3 (-0.5f, 0, 0));
			}
			else if (!map.HasContent(pos + new Position (1, 0, 0))) {
				doorPortal.transform.Rotate(0,0,0);
				doorPortal.transform.position = map.Scaled(pos.Vector3 + new Vector3 (0.5f, 0, 0));
			}
			else if (!map.HasContent(pos + new Position (0, 0, -1))) {
				doorPortal.transform.Rotate(0,90,0);
				doorPortal.transform.position = map.Scaled(pos.Vector3 + new Vector3 (0, 0, -0.5f));
			}
			else if (!map.HasContent(pos + new Position (0, 0, 1))) {
				doorPortal.transform.Rotate(0,270,0);
				doorPortal.transform.position = map.Scaled(pos.Vector3 + new Vector3 (0, 0, 0.5f));
			}
		}
	}
}
