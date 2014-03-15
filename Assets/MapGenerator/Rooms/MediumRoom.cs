using System;


namespace MyNameSpace
{
	public class MediumRoom : Room
	{
		public MediumRoom (Random r_in, int level) : base (12, 12, 2, 5, r_in) {
			this.level = level;
		}
	}
}

