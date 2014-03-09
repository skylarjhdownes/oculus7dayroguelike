using UnityEngine;
using System.Collections;

public class smallEmptyRoom : room {
	private GameObject self = new GameObject("Hallways");
	private Texture wallTexture;
	private Texture floorTexture;
	private Texture ceilingTexture;
	
	public smallEmptyRoom (Texture wallTexture, Texture ceilingTexture, Texture floorTexture) {
		this.wallTexture = wallTexture;
		this.ceilingTexture = ceilingTexture;
		this.floorTexture = floorTexture;
	}
	
	public void RenderRoom(int x, int y, int z, dungeonMap d) {
		Transform wallPos;
		
		UnityEngine.Debug.Log ("Texture is " + wallTexture);
		
		// floor
		if (!NeighborExists(x,y-1,z,d)) {
			wallPos = newFloor();
			wallPos.Rotate(90,0,0);
			wallPos.position = new Vector3 (x, y-0.5f, z);
		}
		
		// ceiling
		if (!NeighborExists(x,y+1,z,d)) {
			wallPos = newCeiling();
			wallPos.Rotate(-90,0,0);
			wallPos.position = new Vector3 (x, y+0.5f, z);
		}
		
		// walls
		if (!NeighborExists(x-1,y,z,d)) {
			wallPos = newWall ();
			wallPos.Rotate(0,-90,0);
			wallPos.position = new Vector3 (x-0.5f, y, z);
		}
		if (!NeighborExists(x+1,y,z,d)) {
			wallPos = newWall ();
			wallPos.Rotate(0,90,0);
			wallPos.position = new Vector3 (x+0.5f, y, z);
		}
		if (!NeighborExists(x,y,z-1,d)) {
			wallPos = newWall ();
			wallPos.Rotate(0,180,0);
			wallPos.position = new Vector3 (x, y, z-0.5f);
		}
		if (!NeighborExists(x,y,z+1,d)) {
			wallPos = newWall ();
			wallPos.Rotate(0,0,0);
			wallPos.position = new Vector3 (x, y, z+0.5f);
		}
	}
	
	private Transform newWall () {
		GameObject created = GameObject.CreatePrimitive (PrimitiveType.Quad);
		created.transform.parent = self.transform;
		created.renderer.material.mainTexture = wallTexture;
		return created.transform;
	}

	private Transform newCeiling () {
		GameObject created = GameObject.CreatePrimitive (PrimitiveType.Quad);
		created.transform.parent = self.transform;
		created.renderer.material.mainTexture = ceilingTexture;
		return created.transform;
	}

	private Transform newFloor () {
		GameObject created = GameObject.CreatePrimitive (PrimitiveType.Quad);
		created.transform.parent = self.transform;
		created.renderer.material.mainTexture = floorTexture;
		return created.transform;
	}

	public bool NeighborExists(int x, int y, int z,  dungeonMap d) {
		// if out of bounds, no neighbor
		if (x < 0 || y < 0 || z < 0 || d.map.GetLength (0) <= x || d.map.GetLength (1) <= y || d.map.GetLength (2) <= z) {
			return false;
		}
		// if not null, neighbor
		return d.map [x, y, z] != null;
	}
}
