namespace MyNameSpace {

	public class Rect {
		
		public int startX;
		public int startY;
		public int finX;
		public int finY;
		
		public Rect(int sX, int sY, int fX, int fY) {
			startX = sX;
			startY = sY;
			finX = fX;
			finY = fY;
		}
		
		public bool equals(Rect rect_in) {
			return 
				( rect_in.startX == startX ) &&
					( rect_in.startY == startY ) &&
					( rect_in.finX == finX ) &&
					( rect_in.finY == finY );
		}
		
		public string toString() {
			return "["+startX+","+startY+"] -> ["+finX+","+finY+"]";
		}

		public bool intersects(Rect r_in) {
			Point L1p1, L1p2,L2p1,L2p2;

			// Check Line #1
			L1p1 = new Point(startX,startY);
			L1p2 = new Point(finX,startY);
			L2p1 = new Point(r_in.startX,r_in.startY);
			L2p2 = new Point(r_in.startX,r_in.finY);

			if ( r_in.startY <= startY && startY <= r_in.finY && startX <= r_in.startX && r_in.startX <= finX ) return true;
			if ( r_in.startY <= startY && startY <= r_in.finY && startX <= r_in.finX && r_in.finX <= finX ) return true;
			if ( r_in.startY <= finY && finY <= r_in.finY && startX <= r_in.startX && r_in.startX <= finX ) return true;
			if ( r_in.startY <= finY && finY <= r_in.finY && startX <= r_in.finX && r_in.finX <= finX ) return true;


			return false;
		}
	}

}