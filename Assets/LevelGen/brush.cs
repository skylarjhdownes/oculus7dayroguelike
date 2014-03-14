using UnityEngine;
using System.Collections.Generic;
namespace LevelGen {
	public interface Brush {
		void RenderRoom(Position position, Dungeon map);
	}
}