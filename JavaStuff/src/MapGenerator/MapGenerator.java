package MapGenerator;

import java.util.LinkedList;
import java.util.Random;

public class MapGenerator {
	
	static Random r = new Random((long)(Math.random()*99999999));
	static long seed = -1;
    
	public static Map generateMapWithRectangularRoomsFirst(int x, int y, double roomDensity, long seed) {
		MapGenerator.seed = seed;
		r = new Random(seed);
		return generateMapWithRectangularRoomsFirst(x,y,roomDensity);
	}
	
	public static Map generateMapWithRectangularRoomsFirst(int x, int y, double roomDensity) {

		Map nMap = new Map(x,y);
		
		
		double cRD = 0.0;
		
		int startX, startY, finishX, finishY, tempX, tempY;
		boolean done;
		while ( cRD < roomDensity ) {
			
			done = false;
			
			// Find a random X, Y
			do {
				startX = (int)(r.nextDouble()*x);
				startY = (int)(r.nextDouble()*y);				
			} while ( nMap.MapGrid[startX][startY] != 99 );
			
			finishX = startX;
			finishY = startY;
			tempX = finishX;
			tempY = finishY;
			
			double expand;
			while ( !done ) {
				// Choose a random direction to expand in
				expand = r.nextDouble();
				if ( expand < .1 ) {
					done = true;
					continue;
				}
				if ( expand < .55 ) { if ( tempX < x-1 ) tempX++; }
				else { if ( tempY < y-1) tempY++; }
				
				if ( nMap.MapGrid[tempX][tempY] != 99 ) {
					done = true;
					continue;
				}
				
				finishX = tempX;
				finishY = tempY;
			}
			
			if ( ( (finishX-startX) < 2 ) || ( (finishY-startY) < 2) ) { continue; }
			
			for ( int i = startX; i <= finishX; i++ ) {
				for ( int j = startY; j <= finishY; j++ ) {
					nMap.MapGrid[i][j] = 0;
					cRD += (1.0/(x*y));
				}
			}
			Rect t = new Rect(startX,startY,finishX,finishY);
			nMap.Rooms.add(t);
		}
		
		
		//buildRandomCorridorsFromAllRoomsToAllRooms(nMap);
		if ( seed != -1 ) r = new Random(seed);
		buildRandomCorridorsFromAllRoomsToOneRoom(nMap);
		
		return nMap;
	}
	
	public static Map getMapWithRoomsFrom(Map in) {
		Map nMap = new Map(in.width,in.height);
		
		for ( Rect n : in.Rooms ) {
			nMap.Rooms.add(n);
			for ( int i = n.startX; i <= n.finX; i++ ) {
				for ( int j = n.startY; j <= n.finY; j++ ) {
					nMap.MapGrid[i][j] = 0;
				}
			}
		}
		
		return nMap;
	}

	public static void buildRandomCorridorsFromAllRoomsToAllRooms(Map nMap,long seed) {
		r = new Random(seed);
		buildRandomCorridorsFromAllRoomsToAllRooms(nMap);
	}

	public static void buildRandomCorridorsFromAllRoomsToAllRooms(Map nMap) {
		int mpX1, mpX2, mpY1, mpY2, cY,cX;
		for ( Rect n : nMap.Rooms ) {
			for ( Rect n2 : nMap.Rooms ) {
				if ( !n.equals(n2) ) {
					//System.out.println("N > N2");
					
					// Find the mid points of both rectangles
					mpX1 = (int)(((n.finX-n.startX)/2.0)+n.startX);
					mpY1 = (int)(((n.finY-n.startY)/2.0)+n.startY);
					mpX2 = (int)(((n2.finX-n2.startX)/2.0)+n2.startX);
					mpY2 = (int)(((n2.finY-n2.startY)/2.0)+n2.startY);
					
					// Randomly choose a cross-point
					if ( r.nextDouble() > .5 ) {
						cX = mpX1;
						cY = mpY2;
					}
					else {
						cX = mpX2;
						cY = mpY1;						
					}
					
					//System.out.println("R1 :"+n+"     R2: "+n2+"     C: "+cX+","+cY);
					
					// Fill in the corridor
					if ( cX < mpX1 ) { } for ( int i = cX; i <= mpX1; i++ ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2; 
					if ( cX > mpX1 ) { } for ( int i = cX; i >= mpX1; i-- ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2;
					if ( cX < mpX2 ) { } for ( int i = cX; i <= mpX2; i++ ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2; 
					if ( cX > mpX2 ) { } for ( int i = cX; i >= mpX2; i-- ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2;

					if ( cY < mpY1 ) { } for ( int i = cY; i <= mpY1; i++ ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2; 
					if ( cY > mpY1 ) { } for ( int i = cY; i >= mpY1; i-- ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2;
					if ( cY < mpY2 ) { } for ( int i = cY; i <= mpY2; i++ ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2; 
					if ( cY > mpY2 ) { } for ( int i = cY; i >= mpY2; i-- ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2;
}
			}
		}

	}

	public static void buildRandomCorridorsFromAllRoomsToOneRoom(Map nMap,long seed) {
		r = new Random(seed);
		buildRandomCorridorsFromAllRoomsToOneRoom(nMap);
	}
	
	public static void buildRandomCorridorsFromAllRoomsToOneRoom(Map nMap) {
		int mpX1, mpX2, mpY1, mpY2, cY,cX;
		
		int rCount;
		double distance;
		boolean continueFlag;
		for ( Rect n : nMap.Rooms ) {
			rCount = nMap.Rooms.size();
			continueFlag = false;
			
			for ( Rect n2 : nMap.Rooms ) {
				if ( continueFlag ) continue;
				
				// Find the mid points of both rectangles
				mpX1 = (int)(((n.finX-n.startX)/2.0)+n.startX);
				mpY1 = (int)(((n.finY-n.startY)/2.0)+n.startY);
				mpX2 = (int)(((n2.finX-n2.startX)/2.0)+n2.startX);
				mpY2 = (int)(((n2.finY-n2.startY)/2.0)+n2.startY);
				distance = Math.sqrt(((mpY1-mpY2)*(mpY1-mpY2))+((mpX1-mpX2)*(mpX1-mpX2)));

				if ( r.nextInt(rCount) != 0 ) {
					rCount--;
					continue;
				}
				if ( distance >= 45 ) {
					rCount--;
					continue;
				}
				continueFlag = true;
				
				
				if ( !n.equals(n2) ) {
					//System.out.println("N > N2");
					
					
					// Randomly choose a cross-point
					if ( r.nextDouble() > .5 ) {
						cX = mpX1;
						cY = mpY2;
					}
					else {
						cX = mpX2;
						cY = mpY1;						
					}
					
					//System.out.println("R1 :"+n+"     R2: "+n2+"     C: "+cX+","+cY);
					
					// Fill in the corridor
					if ( cX < mpX1 ) { } for ( int i = cX; i <= mpX1; i++ ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2; 
					if ( cX > mpX1 ) { } for ( int i = cX; i >= mpX1; i-- ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2;
					if ( cX < mpX2 ) { } for ( int i = cX; i <= mpX2; i++ ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2; 
					if ( cX > mpX2 ) { } for ( int i = cX; i >= mpX2; i-- ) if ( nMap.MapGrid[i][cY] == 99 ) if ( nMap.MapGrid[i][cY+1] != 2 ) if ( nMap.MapGrid[i][cY-1] != 2 ) nMap.MapGrid[i][cY] = 2;

					if ( cY < mpY1 ) { } for ( int i = cY; i <= mpY1; i++ ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2; 
					if ( cY > mpY1 ) { } for ( int i = cY; i >= mpY1; i-- ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2;
					if ( cY < mpY2 ) { } for ( int i = cY; i <= mpY2; i++ ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2; 
					if ( cY > mpY2 ) { } for ( int i = cY; i >= mpY2; i-- ) if ( nMap.MapGrid[cX][i] == 99 ) if ( nMap.MapGrid[cX+1][i] != 2 ) if ( nMap.MapGrid[cX-1][i] != 2 ) nMap.MapGrid[cX][i] = 2;
}
			}
			
		}

	}


	public static class Rect {
		
		int startX;
		int startY;
		int finX;
		int finY;
		
		public Rect(int sX, int sY, int fX, int fY) {
			startX = sX;
			startY = sY;
			finX = fX;
			finY = fY;
		}
		
		public boolean equals(Rect in) {
			return 
					( in.startX == startX ) &&
					( in.startY == startY ) &&
					( in.finX == finX ) &&
					( in.finY == finY );
		}
		
		public String toString() {
			return "["+startX+","+startY+"] -> ["+finX+","+finY+"]";
		}
	}
}


