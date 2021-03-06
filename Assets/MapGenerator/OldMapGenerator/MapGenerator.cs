using System;
using System.Collections;
using MyNameSpace;
namespace OLDMyNameSpace {

	public class MapGenerator {
		
		static Random r = new Random(5);
		static int seed = -1;
		
		public static Map generateMapWithRectangularRoomsFirst(int x, int y, double roomDensity, int seed) {
			MapGenerator.seed = seed;
			r = new Random(seed);
			return generateMapWithRectangularRoomsFirst(x,y,roomDensity);
		}
		
		public static Map generateMapWithRectangularRoomsFirst(int x, int y, double roomDensity) {
			
			Map nMap = new Map(x,y);
			
			
			double cRD = 0.0;
			
			int startX, startY, finishX, finishY, tempX, tempY;
			bool done;
			while ( cRD < roomDensity ) {
				
				done = false;
				
				// Find a random X, Y
				do {
					startX = (int)(r.NextDouble()*x);
					startY = (int)(r.NextDouble()*y);				
				} while ( nMap.MapGrid[startX,startY] != 99 );
				
				finishX = startX;
				finishY = startY;
				tempX = finishX;
				tempY = finishY;
				
				double expand;
				while ( !done ) {
					// Choose a random direction to expand in
					expand = r.NextDouble();
					if ( expand < .1 ) {
						done = true;
						continue;
					}
					if ( expand < .55 ) { if ( tempX < x-1 ) tempX++; }
					else { if ( tempY < y-1) tempY++; }
					
					if ( nMap.MapGrid[tempX,tempY] != 99 ) {
						done = true;
						continue;
					}
					
					finishX = tempX;
					finishY = tempY;
				}
				
				if ( ( (finishX-startX) < 2 ) || ( (finishY-startY) < 2) ) { continue; }
				
				for ( int i = startX; i <= finishX; i++ ) {
					for ( int j = startY; j <= finishY; j++ ) {
						nMap.MapGrid[i,j] = 0;
						cRD += (1.0/(x*y));
					}
				}
				Rect t = new Rect(startX,startY,finishX,finishY);
				nMap.Rooms.Add(t);
			}
			
			
			//buildRandomCorridorsFromAllRoomsToAllRooms(nMap);
			if ( seed != -1 ) r = new Random(seed);
			buildRandomCorridorsFromAllRoomsToOneRoom(nMap);
			
			return nMap;
		}
		
		public static Map getMapWithRoomsFrom(Map map_in) {
			Map nMap = new Map(map_in.width,map_in.height);
			
			foreach ( Rect n in map_in.Rooms ) {
				nMap.Rooms.Add(n);
				for ( int i = n.startX; i <= n.finX; i++ ) {
					for ( int j = n.startY; j <= n.finY; j++ ) {
						nMap.MapGrid[i,j] = 0;
					}
				}
			}
			
			return nMap;
		}
		
		public static void buildRandomCorridorsFromAllRoomsToAllRooms(Map nMap,int seed) {
			r = new Random(seed);
			buildRandomCorridorsFromAllRoomsToAllRooms(nMap);
		}
		
		public static void buildRandomCorridorsFromAllRoomsToAllRooms(Map nMap) {
			int mpX1, mpX2, mpY1, mpY2, cY,cX;
			foreach ( Rect n in nMap.Rooms ) {
				foreach ( Rect n2 in nMap.Rooms ) {
					if ( !n.equals(n2) ) {
						//System.out.println("N > N2");
						
						// Find the mid points of both rectangles
						mpX1 = (int)(((n.finX-n.startX)/2.0)+n.startX);
						mpY1 = (int)(((n.finY-n.startY)/2.0)+n.startY);
						mpX2 = (int)(((n2.finX-n2.startX)/2.0)+n2.startX);
						mpY2 = (int)(((n2.finY-n2.startY)/2.0)+n2.startY);
						
						// Randomly choose a cross-point
						if ( r.NextDouble() > .5 ) {
							cX = mpX1;
							cY = mpY2;
						}
						else {
							cX = mpX2;
							cY = mpY1;						
						}
						
						//System.out.println("R1 :"+n+"     R2: "+n2+"     C: "+cX+","+cY);
						
						// Fill in the corridor
						if ( cX < mpX1 ) { } for ( int i = cX; i <= mpX1; i++ ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2; 
						if ( cX > mpX1 ) { } for ( int i = cX; i >= mpX1; i-- ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2;
						if ( cX < mpX2 ) { } for ( int i = cX; i <= mpX2; i++ ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2; 
						if ( cX > mpX2 ) { } for ( int i = cX; i >= mpX2; i-- ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2;
						
						if ( cY < mpY1 ) { } for ( int i = cY; i <= mpY1; i++ ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2; 
						if ( cY > mpY1 ) { } for ( int i = cY; i >= mpY1; i-- ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2;
						if ( cY < mpY2 ) { } for ( int i = cY; i <= mpY2; i++ ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2; 
						if ( cY > mpY2 ) { } for ( int i = cY; i >= mpY2; i-- ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2;
					}
				}
			}
			
		}
		
		public static void buildRandomCorridorsFromAllRoomsToOneRoom(Map nMap,int seed) {
			r = new Random(seed);
			buildRandomCorridorsFromAllRoomsToOneRoom(nMap);
		}

		public static void buildRandomCorridorsFromAllRoomsToOneRoom(Map nMap) {
			int mpX1, mpX2, mpY1, mpY2, cY,cX;
			
			int rCount;
			double distance;
			bool continueFlag;
			foreach ( Rect n in nMap.Rooms ) {
				mpX1 = (int)(((n.finX-n.startX)/2.0)+n.startX);
				mpY1 = (int)(((n.finY-n.startY)/2.0)+n.startY);
				
				// Make a list of rooms within the proper distance of this one.
				ArrayList closeRooms = new ArrayList();
				foreach ( Rect n2 in nMap.Rooms ) {
					mpX2 = (int)(((n2.finX-n2.startX)/2.0)+n2.startX);
					mpY2 = (int)(((n2.finY-n2.startY)/2.0)+n2.startY);
					distance = Math.Sqrt(((mpY1-mpY2)*(mpY1-mpY2))+((mpX1-mpX2)*(mpX1-mpX2)));
					
					if ( distance < 45 ) closeRooms.Add(n2);
				}

				rCount = closeRooms.Count;
				continueFlag = false;
				
				foreach ( Rect n2 in closeRooms ) {
					if ( continueFlag ) continue;
					
					// Find the mid points of both rectangles

					if ( r.Next(rCount) != 0 ) {
						rCount--;
						continue;
					}
					continueFlag = true;
					mpX2 = (int)(((n2.finX-n2.startX)/2.0)+n2.startX);
					mpY2 = (int)(((n2.finY-n2.startY)/2.0)+n2.startY);
					
					
					if ( !n.equals(n2) ) {
						//System.out.println("N > N2");
						
						
						// Randomly choose a cross-point
						if ( r.NextDouble() > .5 ) {
							cX = mpX1;
							cY = mpY2;
						}
						else {
							cX = mpX2;
							cY = mpY1;						
						}
						
						//System.out.println("R1 :"+n+"     R2: "+n2+"     C: "+cX+","+cY);
						
						// Fill in the corridor
						if ( cX < mpX1 ) { } for ( int i = cX; i <= mpX1; i++ ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2; 
						if ( cX > mpX1 ) { } for ( int i = cX; i >= mpX1; i-- ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2;
						if ( cX < mpX2 ) { } for ( int i = cX; i <= mpX2; i++ ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2; 
						if ( cX > mpX2 ) { } for ( int i = cX; i >= mpX2; i-- ) if ( nMap.MapGrid[i,cY] == 99 ) if ( nMap.MapGrid[i,cY+1] != 2 ) if ( nMap.MapGrid[i,cY-1] != 2 ) nMap.MapGrid[i,cY] = 2;
						
						if ( cY < mpY1 ) { } for ( int i = cY; i <= mpY1; i++ ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2; 
						if ( cY > mpY1 ) { } for ( int i = cY; i >= mpY1; i-- ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2;
						if ( cY < mpY2 ) { } for ( int i = cY; i <= mpY2; i++ ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2; 
						if ( cY > mpY2 ) { } for ( int i = cY; i >= mpY2; i-- ) if ( nMap.MapGrid[cX,i] == 99 ) if ( nMap.MapGrid[cX+1,i] != 2 ) if ( nMap.MapGrid[cX-1,i] != 2 ) nMap.MapGrid[cX,i] = 2;
					}
				}
				
			}
		}
	}

}