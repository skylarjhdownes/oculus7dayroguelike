using UnityEngine;
using System.Collections.Generic;

public class dungeonMap {
	private List<renderRequest>[,,] map;
	
	public dungeonMap(int xsize, int ysize, int zsize) {
		map = new List<renderRequest> [xsize, ysize, zsize];
	}

	public void RenderAll() {
		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				for (int z = 0; z < map.GetLength (2); z++) {
					Vector3 currentPosition = new Vector3 (x, y, z);
					foreach (renderRequest r in getPosition(currentPosition)) {
						r.render(currentPosition);
					}
				}
			}
		}
	}
	public List<renderRequest> getPosition(Vector3 position){
		List<renderRequest> cube = map [(int)position.x, (int)position.y, (int)position.z];
		if (cube == null) {
						cube = new List<renderRequest> ();
						map [(int)position.x, (int)position.y, (int)position.z] = cube;
				}
		return cube;
	}

	public void place(int x, int y, int z, brush brush) {
		place (new Vector3 (x, y, z), new Vector3 (1, 1, 1), brush);
	}

	public void place(Vector3 position, Vector3 size, brush brush) {
				getPosition (position).Add (new renderRequest (size, brush));
		}

	public bool inBounds(Vector3 position) {
		return position.x >= 0
						&& position.y >= 0 
						&& position.z >= 0 
						&& map.GetLength (0) > position.x 
						&& map.GetLength (1) > position.y 
						&& map.GetLength (2) > position.z;
	}
}