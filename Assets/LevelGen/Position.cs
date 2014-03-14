using UnityEngine;
using System.Collections.Generic;
namespace LevelGen {
public class Position {
	public readonly int X;
	public readonly int Y;
	public readonly int Z;

	public Vector3 Vector3{
		get {
			return new Vector3(X, Y, Z);
		}
	}
	
	public Position (int x, int y, int z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}
	
	public override string ToString ()
	{
		return string.Format ("[Position: X={0}, Y={1}, Z={2}]", X, Y, Z);
	}
	
	public override bool Equals (object obj)
	{
		if (obj == null)
			return false;
		if (ReferenceEquals (this, obj))
			return true;
		if (obj.GetType () != typeof(Position))
			return false;
		Position other = (Position)obj;
		return X == other.X && Y == other.Y && Z == other.Z;
	}
	

	public override int GetHashCode ()
	{
		unchecked {
			return X.GetHashCode () ^ Y.GetHashCode () ^ Z.GetHashCode ();
		}
	}
	

	public static Position operator +(Position p1, Position p2) {
		return new Position (p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
	}

	public static Position operator -(Position p1, Position p2) {
		return new Position (p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
	}
}
}