using UnityEngine;
using System.Collections;
namespace LevelGen {

public class TestGenerator : MonoBehaviour {
	private Dungeon d;
	public int seed;

	// Use this for initialization
	void Start() {
		d = new Dungeon ();
		var f = new RoomBrushFactory ();
		var rng = new System.Random (seed);
		Brush h = f.createRoomBrush (rng);
		d.Place (new Position(0, 0, 1), h);
		d.Place (new Position(1, 0, 0), h);
		d.Place (new Position(1, 1, 0), h);
		d.Place (new Position(1, 1, 1), h);
		d.Place (new Position(0, 1, 0), h);
		d.Place (new Position(0, 1, 1), h);
		d.RenderAll ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}

}