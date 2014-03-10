using System;
using System.Collections;

namespace MyNameSpace
{
	public abstract class Room
	{
		int height;
		int maxWidth;
		int maxLength;
		int numRectangles;
		public Point ActualLocation;
		public int level;
		public ArrayList listOfRectangles;
		Random r;

		public Room(int l, int w, int h, int r, Random r_in) {
			height = h;
			maxWidth = w;
			maxLength = l;
			numRectangles = r;
			listOfRectangles = new ArrayList ();
			this.r = r_in;
			build ();
		}

		public void setLevel(int l) {
			level = l;
		}

		private void build() {
			int sX, sY, fX, fY;
			for (int i = 0; i < numRectangles; i++) {
				// Build a rectangle
				sX = r.Next(maxWidth-3);
				sY = r.Next(maxLength-3);
				do { fX = r.Next (maxWidth); } while(fX-sX < 3);
				do { fY = r.Next (maxLength); } while(fY-sY < 3);

				listOfRectangles.Add (new Rect(sX,sY, fX, fY));
			}
		}

		public int getMaxWidth() { return maxWidth; }
		public int getMaxLength() { return maxLength; }

	}
}

