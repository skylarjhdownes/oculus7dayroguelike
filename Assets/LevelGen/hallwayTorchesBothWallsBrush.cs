using UnityEngine;
using System.Collections;


public class hallwayTorchesBothWallsBrush : brush {
	private GameObject self;
	public Material wallMaterial;
	public Material floorMaterial;
	public Material ceilingMaterial;
	private static GameObject clonableTorch;
	private dungeonMap map;
	
	public static hallwayTorchesBothWallsBrush prepare(dungeonMap map) {
		GameObject levelGen = GameObject.Find ("LevelGeneration"); //Is dumb, should fix
		hallwayTorchesBothWallsBrush hallwayObject = levelGen.GetComponent<hallwayTorchesBothWallsBrush> ();
		clonableTorch = (GameObject)Resources.Load("ourTorch1");
		Debug.Log (clonableTorch);
		if (hallwayObject == null) {
			UnityEngine.Debug.Log("Please attach the hallwayTorchesBothWallsBrush script to your LevelGeneration object.");
		}
		hallwayObject.map = map;
		hallwayObject.self = new GameObject ("hallwayTorchesBothWallsBrush");

		return hallwayObject;
	}
	
	public override void RenderRoom(Vector3 roomPosition, Vector3 roomSize) {
		Transform wallPos;
		Transform torchPos;


		UnityEngine.Debug.Log ("Texture is " + wallMaterial);
		
		
		// floor
		if (!NeighborExists(roomPosition + new Vector3 (0, -1, 0))) {
			wallPos = newPlane (floorMaterial);
			wallPos.Rotate(90,0,0);
			wallPos.position = roomPosition + new Vector3 (0, -0.5f, 0);
		}
		
		// ceiling
		if (!NeighborExists(roomPosition + new Vector3 (0, 1, 0))) {
			wallPos = newPlane (ceilingMaterial);
			wallPos.Rotate(-90,0,0);
			wallPos.position = roomPosition + new Vector3 (0, 0.5f, 0);
		}
		
		// walls
		if (!NeighborExists(roomPosition + new Vector3 (-1, 0, 0))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,-90,0);
			wallPos.position = roomPosition + new Vector3 (-0.5f, 0, 0);

			torchPos = newTorch();
//			torchPos.parent = wallPos;
//			torchPos.position = new Vector3 (0, 0, 0);
//			torchPos.Rotate(0,-90,0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (1, 0, 0))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,90,0);
			wallPos.position = roomPosition + new Vector3 (0.5f, 0, 0);

//			torchPos = newTorch();
//			torchPos.parent = wallPos;
//			torchPos.position = new Vector3 (0, 0, 0);
//			torchPos.Rotate(0,-90,0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (0, 0, -1))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,180,0);
			wallPos.position = roomPosition + new Vector3 (0, 0, -0.5f);

//			torchPos = newTorch();
//			torchPos.parent = wallPos;
//			torchPos.position = new Vector3 (0, 0, 0);
//			torchPos.Rotate(0,-90,0);
		}
		if (!NeighborExists(roomPosition + new Vector3 (0, 0, 1))) {
			wallPos = newPlane (wallMaterial);
			wallPos.Rotate(0,0,0);
			wallPos.position = roomPosition + new Vector3 (0, 0, 0.5f);

//			torchPos = newTorch();
//			torchPos.parent = wallPos;
//			torchPos.position = new Vector3 (0, 0, 0);
//			torchPos.Rotate(0,-90,0);
		}
	}
	
	private Transform newPlane (Material surfaceMaterial) {
		GameObject createdPlane = GameObject.CreatePrimitive (PrimitiveType.Quad);
		createdPlane.transform.parent = self.transform;
		createdPlane.renderer.material =  surfaceMaterial;
		return createdPlane.transform;
	}

	private Transform newTorch () {
		GameObject createdTorch = (GameObject)Instantiate(clonableTorch);
//		createdTorch.transform.parent = self.transform;
		return createdTorch.transform;
	}
	
	public bool NeighborExists(Vector3 position) {
		// if out of bounds, no neighbor
		if (!map.inBounds(position)) {
			return false;
		}
		return map.getPosition(position).Count > 0;
	}
}