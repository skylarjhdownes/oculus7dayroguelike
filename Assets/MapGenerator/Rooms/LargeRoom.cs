using System;


namespace MyNameSpace
{
	public class LargeRoom : Room
	{
		public LargeRoom (Random r_in, int level) : base (24, 24, 2, 8, r_in) {
			this.level = level;
		}

		public override Room newInstance(Random r_in, int level) {
			return new LargeRoom (r_in, level);
		}

	}
}

