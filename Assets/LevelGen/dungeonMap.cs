using UnityEngine;
using System.Collections.Generic;

public class dungeonMap {
	public room[,,] map;
	
	public dungeonMap(int xsize, int ysize, int zsize) {
		map = new room [xsize, ysize, zsize];
	}

	public void RenderAll() {
		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				for (int z = 0; z < map.GetLength (2); z++) {
					if (map[x,y,z] != null) {
						map[x,y,z].RenderRoom(x,y,z,this);
					}
				}
			}
		}
	}
}