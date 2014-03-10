using UnityEngine;
using System.Collections.Generic;

public class hallwayBrush : paintRoom {
	private GameObject self;
	public Material wallMaterial;
	public Material floorMaterial;
	public Material ceilingMaterial;
	private dungeonMap map;

	public static hallwayBrush prepare(dungeonMap map) {
		GameObject levelGen = GameObject.Find ("LevelGeneration"); //Is dumb, should fix
		hallwayBrush hallwayObject = levelGen.GetComponent<hallwayBrush> ();
		if (hallwayObject == null) {
			UnityEngine.Debug.Log("Please attach the hallways object to your LevelGeneration object.");
		}
		hallwayObject.map = map;
		hallwayObject.self = new GameObject ("Hallways");
		return hallwayObject;
	}
	
	public override void RenderRoom(Vector3 roomPosition, Vector3 roomSize) {
		Transform wallPos;
		
		UnityEngine.Debug.Log ("Texture is " + wallMaterial);
		
		// floor
		if (!NeighborExists(roomPosition + new Vector3 (0, -1, 0))) {
			wallPos = newPlane(floorMaterial);
			wallPos.Rotate(90,0,0);
			wallPos.position = roomPosition + new Vector3 (0, -0.5f, 0);
		}
		
		// ceiling
		if (!NeighborExists(roomPosition + new Vector3 (0, 1, 0))) {
			wallPos = newPlane(ceilingMaterial);
			wallPos.Rotate(-90,0,0);
			wallPos.position = roomPosition + new Vector3 (0, 0.5f, 0);
		}
		
		// walls
		if (!NeighborExists(roomPosition + new Vector3 (-1, 0, 0))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,-90,0);
			wallPos.position = roomPosition + new Vector3 (-0.5f, 0, 0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (1, 0, 0))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,90,0);
			wallPos.position = roomPosition + new Vector3 (0.5f, 0, 0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (0, 0, -1))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,180,0);
			wallPos.position = roomPosition + new Vector3 (0, 0, -0.5f);
		}
		if (!NeighborExists(roomPosition + new Vector3 (0, 0, 1))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,0,0);
			wallPos.position = roomPosition + new Vector3 (0, 0, 0.5f);
		}
	}
	
	private Transform newPlane (Material surfaceMaterial) {
		GameObject created = GameObject.CreatePrimitive (PrimitiveType.Quad);
		created.transform.parent = self.transform;
		created.renderer.material =  surfaceMaterial;
		return created.transform;
	}
	
	public bool NeighborExists(Vector3 position) {
		// if out of bounds, no neighbor
		if (!map.inBounds(position)) {
			return false;
		}
		return map.getPosition(position).Count > 0;
	}
}