using UnityEngine;
using System.Collections.Generic;

public class hallways : room {
	private GameObject self;
	public Texture wallTexture;
	public Texture floorTexture;
	public Texture ceilingTexture;
	private dungeonMap map;

	public static hallways prepare(dungeonMap map) {
		GameObject levelGen = GameObject.Find ("LevelGeneration"); //Is dumb, should fix
		hallways hallwayObject = levelGen.GetComponent<hallways> ();
		hallwayObject.map = map;
		hallwayObject.self = new GameObject ("Hallways");
		return hallwayObject;
	}
	
	public override void RenderRoom(Vector3 roomPosition, Vector3 roomSize) {
		Transform wallPos;
		
		UnityEngine.Debug.Log ("Texture is " + wallTexture);
		
		// floor
		if (!NeighborExists(roomPosition + new Vector3 (0, -1, 0))) {
			wallPos = newPlane(floorTexture);
			wallPos.Rotate(90,0,0);
			wallPos.position = roomPosition + new Vector3 (0, -0.5f, 0);
		}
		
		// ceiling
		if (!NeighborExists(roomPosition + new Vector3 (0, 1, 0))) {
			wallPos = newPlane(ceilingTexture);
			wallPos.Rotate(-90,0,0);
			wallPos.position = roomPosition + new Vector3 (0, 0.5f, 0);
		}
		
		// walls
		if (!NeighborExists(roomPosition + new Vector3 (-1, 0, 0))) {
			wallPos = newPlane (wallTexture);
			wallPos.Rotate(0,-90,0);
			wallPos.position = roomPosition + new Vector3 (-0.5f, 0, 0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (1, 0, 0))) {
			wallPos = newPlane (wallTexture);
			wallPos.Rotate(0,90,0);
			wallPos.position = roomPosition + new Vector3 (0.5f, 0, 0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (0, 0, -1))) {
			wallPos = newPlane (wallTexture);
			wallPos.Rotate(0,180,0);
			wallPos.position = roomPosition + new Vector3 (0, 0, -0.5f);
		}
		if (!NeighborExists(roomPosition + new Vector3 (0, 0, 1))) {
			wallPos = newPlane (wallTexture);
			wallPos.Rotate(0,0,0);
			wallPos.position = roomPosition + new Vector3 (0, 0, 0.5f);
		}
	}
	
	private Transform newPlane (Texture texture) {
		GameObject created = GameObject.CreatePrimitive (PrimitiveType.Quad);
		created.transform.parent = self.transform;
		created.renderer.material.mainTexture = texture;
		return created.transform;
	}
	
	public bool NeighborExists(Vector3 position) {
		// if out of bounds, no neighbor
		if (position.x < 0
		    || position.y < 0 
		    || position.z < 0 
		    || map.map.GetLength (0) <= position.x 
		    || map.map.GetLength (1) <= position.y 
		    || map.map.GetLength (2) <= position.z) {
			return false;
		}
		// if not null, neighbor
		return map.getPosition(position) != null;
	}
}