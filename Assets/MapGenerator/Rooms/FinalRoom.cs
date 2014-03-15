using System;


namespace MyNameSpace
{
	public class FinalRoom : Room
	{
		public FinalRoom (Random r_in) : base (15, 15, 1, 1, r_in) { }

		protected override void build() {
			listOfRectangles.Add (new Rect (2,0,4,2));
			listOfRectangles.Add (new Rect (0,1,5,1));
			listOfRectangles.Add (new Rect (0,2,0,5));
			listOfRectangles.Add (new Rect (1,5,6,5));
			listOfRectangles.Add (new Rect (12,2,14,4));
			listOfRectangles.Add (new Rect (13,0,13,5));
			listOfRectangles.Add (new Rect (9,0,12,0));
			listOfRectangles.Add (new Rect (9,1,9,6));
			listOfRectangles.Add (new Rect (10,12,12,14));
			listOfRectangles.Add (new Rect (9,13,14,13));
			listOfRectangles.Add (new Rect (14,9,14,12));
			listOfRectangles.Add (new Rect (8,9,13,9));
			listOfRectangles.Add (new Rect (0,10,2,12));
			listOfRectangles.Add (new Rect (1,9,1,14));
			listOfRectangles.Add (new Rect (2,14,5,14));
			listOfRectangles.Add (new Rect (5,8,5,13));
			listOfRectangles.Add (new Rect (6,6,8,8));
		}

		public override void drawRoom (LevelGen.Dungeon d_in, LevelGen.Brush b_in, Map m_in)
		{
			base.drawRoom (d_in, b_in, m_in);

			for ( int i = -1; i > -6; i-- ) {
				for ( int x = 2; x <= 4; x++ ) for ( int y = 0; y <= 2; y++ ) d_in.Place (new LevelGen.Position(ActualLocation.x+x+1,i,ActualLocation.y+y+1),b_in);
				for ( int x = 12; x <= 14; x++ ) for ( int y = 2; y <= 4; y++ ) d_in.Place (new LevelGen.Position(ActualLocation.x+x+1,i,ActualLocation.y+y+1),b_in);
				for ( int x = 10; x <= 12; x++ ) for ( int y = 12; y <= 14; y++ ) d_in.Place (new LevelGen.Position(ActualLocation.x+x+1,i,ActualLocation.y+y+1),b_in);
				for ( int x = 0; x <= 2; x++ ) for ( int y = 10; y <= 12; y++ ) d_in.Place (new LevelGen.Position(ActualLocation.x+x+1,i,ActualLocation.y+y+1),b_in);
			}
		}
	}
}

