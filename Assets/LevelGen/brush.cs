using UnityEngine;
using System.Collections.Generic;
namespace LevelGen {
	public interface Brush {
		void Render(Position position, Dungeon map);
	}
}