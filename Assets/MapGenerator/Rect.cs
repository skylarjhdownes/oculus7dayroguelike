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
	}

}