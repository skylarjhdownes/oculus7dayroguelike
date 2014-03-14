using System;
using System.Collections.Generic;
using System.Collections;

namespace MyNameSpace {
	
	public class Map {

		public short[,] MapGrid;
		public int height;
		public int width;
		public List<Room> Rooms;
		Random r;
		
		public Map(int x,int y, int seed) {
			MapGrid = new short[x,y];
			height = y;
			width = x;
			r = new Random(seed);
			Rooms = new List<Room>();
			
			for ( int i = 0; i<x; i++ ) {
				for ( int j = 0; j<y; j++ ) {
					MapGrid[i,j] = 99;
				}
			}
		}

		public void AddRoom(Room_Type t, int l) {
			Room rm = null;
			switch (t) {
			case Room_Type.SpawnRoom:
				rm = new SpawnRoom(r);
				break;
				
			case Room_Type.SmallRoom:
				rm = new SmallRoom(r);
				break;
				
			case Room_Type.MediumRoom:
				rm = new MediumRoom(r);
				break;

			case Room_Type.LargeRoom:
				rm = new LargeRoom(r);
				break;
			}

			rm.setLevel (l);

			Rooms.Add (rm);
		}

		public void buildMap() {

			/* Room Placement Method #1
			// Build a queue of points on the grid.  This queue will be searched through to find locations to build rooms.
			Queue Q = new Queue();
			Q.Enqueue(new Point(0,0));
			/* */ 

			List<Room> prevTier = null;
			List<Room> currentTier = new List<Room> ();

			bool found;
			Point current = null;
			Room myLinkedRoom = null;
			foreach (Room n in Rooms) {

				// First, check to see if we're still on the same tier of room levels.
				if ( currentTier.Count > 0 && (currentTier[0]).level != n.level ) {
					// If not, bump up the tiers.
					prevTier = currentTier;
					currentTier = new List<Room>();

					// TODO -- Update this section to support fixed tiers AND combined tiers.
				}

				// Add this room to the current tier.
				currentTier.Add (n);

				// Next, find space on the map for the room.
				found = false;
				while ( !found ) {

					/* Room Placement -- METHOD #1 -- This method was fundamentally flawed in that it didnt take into account the connections between rooms, and thus trying to connect the rooms properly became an NP problem.  Ew.  Although it did place the rooms in very tight, efficient clusters.
					// Get the next point to check
					current = (Point)Q.Dequeue();

					// Add the follow points to the Queue.
					Q.Enqueue(new Point(current.x+1,current.y));
					if ( current.x == 0 ) Q.Enqueue(new Point(current.x,current.y+1));

					// Expand from this point, to see if this room can fit at this point.
					found = true;
					for ( int i = current.x; i <= current.x+n.getMaxWidth()+2; i++ ) { 
						if ( MapGrid[i,current.y] < 99 ) found = false;
					}
					for ( int i = current.y; i <= current.y+n.getMaxLength()+2; i++ ) { 
						if ( MapGrid[current.x,i] < 99 ) found = false;
					}
					/* */


					/* Room Placement -- METHOD #2 */
					// First, select a room to link this room to from the previous tier.
					if ( prevTier != null ) {
						myLinkedRoom = prevTier[r.Next(prevTier.Count)];
					}
					else {
						// If there is no previous tier, then this must be the first room.  Draw it in the center of the map.
						current = new Point(width/2,height/2);
						found = true;
						break;
					}


					// Next, select a random location on the square radius to place this room on the map.
					current = getRandomPointOnTheSquareRadiusFor(n,myLinkedRoom);

					// Expand from this point, to see if this room can fit at this point.
					found = true;
					for ( int i = current.x; i <= current.x+n.getMaxWidth()+2; i++ ) { 
						for ( int j = current.y; j <= current.y+n.getMaxLength()+2; j++ ) { 
							if ( MapGrid[i,j] < 99 ) found = false;
						}
					}


					/* */
				}


				// Next, build the outline of the room, reserving the space for this room.
				n.ActualLocation = current;
				for ( int i = current.x; i <= current.x+n.getMaxWidth()+2; i++ ) { 
					for ( int j = current.y; j <= current.y+n.getMaxLength()+2; j++ ) { 
						MapGrid[i,j] = 98;
					}
				}


				// Next, draw all of the rectangles in the room.
				foreach ( Rect rc in n.listOfRectangles ) {
					for ( int i = current.x+rc.startX; i <= current.x+rc.finX; i++ ) 
						for ( int j = current.y+rc.startY; j <= current.y+rc.finY; j++ ) 
							MapGrid[i+1,j+1] = 0;
				}

				// Next, connect the current room to a room from the previous tier (Which is already chosen).
				if ( myLinkedRoom != null ) {
					buildHallway(n,myLinkedRoom);

					/* Print Square Radius
					for ( int i = 0; i < 200; i++ ) {
						Point p = getRandomPointOnTheSquareRadiusFor(n,myLinkedRoom);
						MapGrid[p.x,p.y] = 0;
					}
					/* */

				}





			}
		}

		private Point getRandomPointOnTheSquareRadiusFor(Room outer_Room,Room inner_Room) {
			Point ret = null;

			Point start = inner_Room.ActualLocation;
			int radius = inner_Room.getMaxWidth ()+outer_Room.getMaxWidth ()+4;
			//start = new Point (start.x +inner_Room.getMaxWidth ()+1, start.y + inner_Room.getMaxWidth ()+1);
			start = new Point (start.x-outer_Room.getMaxWidth()-1, start.y-outer_Room.getMaxWidth()-1);

			int randomPoint = r.Next (radius * 4);

			int rLine = randomPoint / radius;
			int rPos = randomPoint % radius;

			switch (rLine) {
			case 0:
				ret = new Point(start.x+rPos,start.y);
				break;

			case 1:
				ret = new Point(start.x+radius,start.y+rPos);
				break;
			
			case 2:
				ret = new Point(start.x+radius-rPos,start.y+radius);
				break;
			
			case 3:
				ret = new Point(start.x,start.y+radius-rPos);
				break;

			default:
				// WTF How did i get here?
				ret = new Point(-1,-1);
				break;
			}

			return ret;
		}

		private void buildHallway(Room r1, Room r2) {
			int mpX1, mpX2, mpY1, mpY2;
			bool stillInRoom, reEnteredRoom;

			// Find the mid points of both rectangles
			mpX1 = r1.ActualLocation.x + (r1.getMaxWidth () / 2 + 1);
			mpY1 = r1.ActualLocation.y + (r1.getMaxLength () / 2 + 1);
			mpX2 = r2.ActualLocation.x + (r2.getMaxWidth () / 2 + 1);
			mpY2 = r2.ActualLocation.y + (r2.getMaxLength () / 2 + 1);

			int cX, cY;
			bool emptyCheck = true;

			// Try Cross-point #1
			cX = mpX1;
			cY = mpY2;

			int delX = -1,delY = -1;
			int stX = mpX1,stY = mpY1;
			if ( cX > mpX1 || cX > mpX2 ) delX = 1;
			if ( cY > mpY1 || cY > mpY2 ) delY = 1;
			if ( cX == mpX1 ) stX = mpX2;
			if ( cY == mpY1 ) stY = mpY2;
			
			
			stillInRoom = true;
			reEnteredRoom = false;
			if ( delX == 1 ) {
				for ( int i = stX; i <= cX; i += delX ) {
					if ( MapGrid[i,cY] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					UnityEngine.Debug.Log ("["+i+","+cY+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			else {
				for ( int i = stX; i >= cX; i += delX ) {
					if ( MapGrid[i,cY] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					UnityEngine.Debug.Log ("["+i+","+cY+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			
			stillInRoom = true;
			reEnteredRoom = false;
			if ( delY == 1 ) {
				for ( int i = stY; i <= cY; i += delY ) {
					if ( MapGrid[cX,i] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					UnityEngine.Debug.Log ("["+cX+","+i+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			else {
				for ( int i = stY; i >= cY; i += delY ) {
					if ( MapGrid[cX,i] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					UnityEngine.Debug.Log ("["+cX+","+i+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			//emptyCheck = false;
			if ( emptyCheck ) {
				//UnityEngine.Debug.Log("EC1 -- > stX: "+stX+"   stY: "+stY+"   delX: "+delX+"   cX: "+cX+"   cY: "+cY+"   delY: "+delY);
				// This path is clear, so build it!
				if ( delX == 1 ) { for ( int i = stX; i <= cX; i += delX ) if ( MapGrid[i,cY] >= 0 ) MapGrid[i,cY] = 2; }
				else {			   for ( int i = stX; i >= cX; i += delX ) if ( MapGrid[i,cY] >= 0 ) MapGrid[i,cY] = 2; }
				if ( delY == 1 ) { for ( int i = stY; i <= cY; i += delY ) if ( MapGrid[cX,i] >= 0 ) MapGrid[cX,i] = 2; }
				else { 			   for ( int i = stY; i >= cY; i += delY ) if ( MapGrid[cX,i] >= 0 ) MapGrid[cX,i] = 2; }
			}
			else {
				// Try Cross-point #2
				cX = mpX2;
				cY = mpY1;						
				
				delX = -1; delY = -1;
				stX = mpX1; stY = mpY1;
				if ( cX > mpX1 || cX > mpX2 ) delX = 1;
				if ( cY > mpY1 || cY > mpY2 ) delY = 1;
				if ( cX == mpX1 ) stX = mpX2;
				if ( cY == mpY1 ) stY = mpY2;
				
				emptyCheck = true;

				stillInRoom = true;
				reEnteredRoom = false;
				if ( delX == 1 ) {
					for ( int i = stX; i <= cX; i += delX ) {
						if ( MapGrid[i,cY] == 99 ) { 
							if ( reEnteredRoom ) emptyCheck = false;
							stillInRoom = false; 
						}
						else { 
							if ( !stillInRoom ) { reEnteredRoom = true; } 
						}
						UnityEngine.Debug.Log ("["+i+","+cY+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
					}
				}
				else {
					for ( int i = stX; i >= cX; i += delX ) {
						if ( MapGrid[i,cY] == 99 ) { 
							if ( reEnteredRoom ) emptyCheck = false;
							stillInRoom = false; 
						}
						else { 
							if ( !stillInRoom ) { reEnteredRoom = true; } 
						}
						UnityEngine.Debug.Log ("["+i+","+cY+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
					}
				}
				
				stillInRoom = true;
				reEnteredRoom = false;
				if ( delY == 1 ) {
					for ( int i = stY; i <= cY; i += delY ) {
						if ( MapGrid[cX,i] == 99 ) { 
							if ( reEnteredRoom ) emptyCheck = false;
							stillInRoom = false; 
						}
						else { 
							if ( !stillInRoom ) { reEnteredRoom = true; } 
						}
						UnityEngine.Debug.Log ("["+cX+","+i+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
					}
				}
				else {
					for ( int i = stY; i >= cY; i += delY ) {
						if ( MapGrid[cX,i] == 99 ) { 
							if ( reEnteredRoom ) emptyCheck = false;
							stillInRoom = false; 
						}
						else { 
							if ( !stillInRoom ) { reEnteredRoom = true; } 
						}
						UnityEngine.Debug.Log ("["+cX+","+i+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
					}
				}

				if ( emptyCheck ) {
					//UnityEngine.Debug.Log("EC2 -- > stX: "+stX+"   stY: "+stY+"   delX: "+delX+"   cX: "+cX+"   cY: "+cY+"   delY: "+delY);
					// This path is clear, so build it!
					if ( delX == 1 ) { for ( int i = stX; i <= cX; i += delX ) if ( MapGrid[i,cY] >= 0 ) MapGrid[i,cY] = 2; }
					else {			   for ( int i = stX; i >= cX; i += delX ) if ( MapGrid[i,cY] >= 0 ) MapGrid[i,cY] = 2; }
					if ( delY == 1 ) { for ( int i = stY; i <= cY; i += delY ) if ( MapGrid[cX,i] >= 0 ) MapGrid[cX,i] = 2; }
					else { 			   for ( int i = stY; i >= cY; i += delY ) if ( MapGrid[cX,i] >= 0 ) MapGrid[cX,i] = 2; }
				}
				else {
					UnityEngine.Debug.Log("Err -- >"+
					                      "   mp1: ["+mpX1+","+mpY1+"]"+
					                      "   mp2: ["+mpX2+","+mpY2+"]"
										);
					UnityEngine.Debug.Log("Unable to draw a hallway!!");
				}
			
			} // End Else -> Cross-Point #2

		}

		

	}


	public class Point {

		public int x,y;

		public Point( int x_in, int y_in ) {
			x = x_in;
			y = y_in;
		}
	}

}