using System;
using System.Collections;


namespace OLDMyNameSpace {
	
	public class Map {

		public short[,] MapGrid;
		public int height;
		public int width;
		public ArrayList Rooms;
		
		public Map(int x,int y) {
			MapGrid = new short[x,y];
			height = y;
			width = x;
			Rooms = new ArrayList();
			
			for ( int i = 0; i<x; i++ ) {
				for ( int j = 0; j<y; j++ ) {
					MapGrid[i,j] = 99;
				}
			}
		}
		
		public void printMap() {
			UnityEngine.Debug.Log("Map Print:\n");
			for ( int i = 0; i < height; i++) {
				UnityEngine.Debug.Log("   ");
				for ( int j = 0; j < width; j++) {
					switch (MapGrid[j,i]) {
					case 0:
						UnityEngine.Debug.Log("# ");
						break;
						
					case 2:
						UnityEngine.Debug.Log("@ ");
						break;
						
					case 99:
						UnityEngine.Debug.Log("  ");
						break;
					}
				}
				UnityEngine.Debug.Log('\n');
			}
		}
		
	}

}