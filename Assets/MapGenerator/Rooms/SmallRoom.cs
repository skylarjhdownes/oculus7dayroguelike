using System;


namespace MyNameSpace
{
	public class SmallRoom : Room
	{
		public SmallRoom (Random r_in, int level) : base (6, 6, 1, 2, r_in) {
			this.level = level;
		}
	}
}

