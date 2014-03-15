using UnityEngine;
using System.Collections;

namespace LevelGen {
	public class torchBrush : Brush {
		private GameObject self;
		private GameObject clonableTorch = (GameObject)Resources.Load("ourTorch1");
		private Dungeon map;
		
		public torchBrush() {
			 
		}
		public void RenderRoom (Position pos, Dungeon map)
		{
			var torch = (GameObject)Object.Instantiate(clonableTorch);
			map.AddChild(torch);
			// walls
			if (!map.HasContent(pos + new Position (-1, 0, 0))) {
				torch.transform.Rotate(0,180,0);
				torch.transform.position = pos.Vector3 + new Vector3 (-0.5f, 0, 0);
			}
			else if (!map.HasContent(pos + new Position (1, 0, 0))) {
				torch.transform.Rotate(0,0,0);
				torch.transform.position = pos.Vector3 + new Vector3 (0.5f, 0, 0);
			}
			else if (!map.HasContent(pos + new Position (0, 0, -1))) {
				torch.transform.Rotate(0,90,0);
				torch.transform.position = pos.Vector3 + new Vector3 (0, 0, -0.5f);
			}
			else if (!map.HasContent(pos + new Position (0, 0, 1))) {
				torch.transform.Rotate(0,270,0);
				torch.transform.position = pos.Vector3 + new Vector3 (0, 0, 0.5f);
			}
		}
	}
}