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
			}

			rm.setLevel (l);

			Rooms.Add (rm);
		}

		public void buildMap() {
			// Build a queue of points on the grid.  This queue will be searched through to find locations to build rooms.
			Queue Q = new Queue();
			Q.Enqueue(new Point(0,0));

			List<Room> prevTier = null;
			List<Room> currentTier = new List<Room> ();

			bool found;
			Point current = null;
			foreach (Room n in Rooms) {

				// First, check to see if we're still on the same tier of room levels.
				if ( currentTier.Count > 0 && (currentTier[0]).level != n.level ) {
					// If not, bump up the tiers.
					prevTier = currentTier;
					currentTier = new List<Room>();
				}

				// Add this room to the current tier.
				currentTier.Add (n);

				// Next, find space on the map for the room.
				found = false;
				while ( !found ) {
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


				// Next, connect the current room to a room from the previous tier.
				if ( prevTier != null ) {
					buildHallway(n,((Room)prevTier[r.Next(prevTier.Count)]));
				}

			}
		}

		private void buildHallway(Room r1, Room r2) {
			
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