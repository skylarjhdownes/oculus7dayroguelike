using System;
using System.Collections.Generic;
using System.Collections;
using LevelGen;

namespace MyNameSpace {
	
	public class Map {
		private readonly Dictionary<Position, short> mapCollisionGrid = new Dictionary<Position, short>(new FlatPositionCompare());
		public List<Room> Rooms;
		private Random rng;
		private RoomBrushFactory brushes;
		private Dungeon target;
		private Brush hallBrush;
		private Brush torchBrush;
		
		public Map(Dungeon target, Random rng, List<Room> rooms) {
			this.rng = rng;
			this.target = target;
			this.brushes = new RoomBrushFactory ();
			this.hallBrush = brushes.createRoomBrush (rng);
			this.torchBrush = new LevelGen.torchBrush ();
			this.Rooms = rooms;
			UnityEngine.Debug.Log (Rooms);
		}

		// The Map contains a collision grid, which allows you to check in 2d what things are overlapping with what other things
		public short this[int x, int z] {
			get {
				var target = new Position (x, z);
				if (mapCollisionGrid.ContainsKey(target))
					return mapCollisionGrid [target];
				else return 99;
			}
			set { mapCollisionGrid [new Position (x, z)] = value; }
		}

		public void buildMap() {

			Rooms.Sort ();

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
						myLinkedRoom = prevTier[rng.Next(prevTier.Count)];
					}
					else {
						// If there is no previous tier, then this must be the first room.  Draw it in the center of the map.
						current = new Point(0, 0);
						found = true;
						break;
					}


					// Next, select a random location on the square radius to place this room on the map.
					current = getRandomPointOnTheSquareRadiusFor(n,myLinkedRoom);

					//UnityEngine.Debug.Log("Trying > "+current.x+","+current.y);


					// Expand from this point, to see if this room can fit at this point.
					found = true;
					for ( int i = current.x; i <= current.x+n.getMaxWidth()+2; i++ ) { 
						for ( int j = current.y; j <= current.y+n.getMaxLength()+2; j++ ) { 
							if ( this[i,j] < 99 ) found = false;
						}
					}


					/* */
				}


				// Next, build the outline of the room, reserving the space for this room.
				n.ActualLocation = current;
				for ( int i = current.x; i <= current.x+n.getMaxWidth()+2; i++ ) { 
					for ( int j = current.y; j <= current.y+n.getMaxLength()+2; j++ ) { 
						this[i,j] = 98;
					}
				}


				// Next, draw all of the rectangles in the room.
				var brush = brushes.createRoomBrush(rng);
				n.drawRoom(target,brush,this);

				// Next, connect the current room to a room from the previous tier (Which is already chosen).
				if ( myLinkedRoom != null ) {
					buildHallway(n,myLinkedRoom);

					/* Print Square Radius
					for ( int i = 0; i < 350; i++ ) {
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

			int lSqOuter = outer_Room.getMaxWidth();
			if ( outer_Room.getMaxLength() > lSqOuter ) lSqOuter = outer_Room.getMaxLength();
			int lSqInner = inner_Room.getMaxWidth();
			if ( inner_Room.getMaxLength() > lSqInner ) lSqInner = inner_Room.getMaxLength();

			int radius = (lSqInner+2)+(lSqOuter+2)+4;
			//start = new Point (start.x +inner_Room.getMaxWidth ()+1, start.y + inner_Room.getMaxWidth ()+1);
			start = new Point (start.x-lSqOuter-4, start.y-lSqOuter-4);

			//UnityEngine.Debug.Log("Start: "+start.x+","+start.y);

			int randomPoint = rng.Next (radius * 4);

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
					if ( this[i,cY] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					//UnityEngine.Debug.Log ("["+i+","+cY+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			else {
				for ( int i = stX; i >= cX; i += delX ) {
					if ( this[i,cY] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					//UnityEngine.Debug.Log ("["+i+","+cY+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			
			stillInRoom = true;
			reEnteredRoom = false;
			if ( delY == 1 ) {
				for ( int i = stY; i <= cY; i += delY ) {
					if ( this[cX,i] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					//UnityEngine.Debug.Log ("["+cX+","+i+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			else {
				for ( int i = stY; i >= cY; i += delY ) {
					if ( this[cX,i] == 99 ) { 
						if ( reEnteredRoom ) emptyCheck = false;
						stillInRoom = false; 
					}
					else { 
						if ( !stillInRoom ) { reEnteredRoom = true; } 
					}
					//UnityEngine.Debug.Log ("["+cX+","+i+"]  ->   sIR: "+stillInRoom+"    eC: "+emptyCheck);
				}
			}
			//emptyCheck = false;
			if ( emptyCheck ) {
				//UnityEngine.Debug.Log("EC1 -- > stX: "+stX+"   stY: "+stY+"   delX: "+delX+"   cX: "+cX+"   cY: "+cY+"   delY: "+delY);
				// This path is clear, so build it!
				if ( delX == 1 ) { for ( int i = stX; i <= cX; i += delX ) if ( this[i,cY] >= 98 ) placeHallAt(i,cY); }
				else {			   for ( int i = stX; i >= cX; i += delX ) if ( this[i,cY] >= 98 ) placeHallAt(i,cY); }
				if ( delY == 1 ) { for ( int i = stY; i <= cY; i += delY ) if ( this[cX,i] >= 98 ) placeHallAt(cX,i); }
				else { 			   for ( int i = stY; i >= cY; i += delY ) if ( this[cX,i] >= 98 ) placeHallAt(cX,i); }
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
						if ( this[i,cY] == 99 ) { 
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
						if ( this[i,cY] == 99 ) { 
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
						if ( this[cX,i] == 99 ) { 
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
						if ( this[cX,i] == 99 ) { 
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
					if ( delX == 1 ) { for ( int i = stX; i <= cX; i += delX ) if ( this[i,cY] >= 98 ) placeHallAt(i,cY); }
					else {			   for ( int i = stX; i >= cX; i += delX ) if ( this[i,cY] >= 98 ) placeHallAt(i,cY); }
					if ( delY == 1 ) { for ( int i = stY; i <= cY; i += delY ) if ( this[cX,i] >= 98 ) placeHallAt(cX,i); }
					else { 			   for ( int i = stY; i >= cY; i += delY ) if ( this[cX,i] >= 98 ) placeHallAt(cX,i); }
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

		private void placeHallAt(int x, int z) {
			this [x, z] = 2;
			target.Place (new Position (x, 0, z), hallBrush);
			if (rng.Next (10) == 0)
					target.Place (new Position (x, 0, z), torchBrush);
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