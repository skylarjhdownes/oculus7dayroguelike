package MapGenerator;

import java.util.LinkedList;

import MapGenerator.MapGenerator.Rect;

public class Map {

	short[][] MapGrid;
	int height;
	int width;
	LinkedList<Rect> Rooms;
	
	public Map(int x,int y) {
		MapGrid = new short[x][y];
		height = y;
		width = x;
		Rooms = new LinkedList<Rect>();
		
		for ( int i = 0; i<x; i++ ) {
			for ( int j = 0; j<y; j++ ) {
				MapGrid[i][j] = 99;
			}
		}
	}
	
	public void printMap() {
		System.out.println("Map Print:");
		for ( int i = 0; i < height; i++) {
			System.out.print("   ");
			for ( int j = 0; j < width; j++) {
				switch (MapGrid[j][i]) {
				case 0:
					System.out.print("# ");
					break;
					
				case 2:
					System.out.print("@ ");
					break;
					
				case 99:
					System.out.print("  ");
					break;
				}
			}
			System.out.println();
		}
	}

}
