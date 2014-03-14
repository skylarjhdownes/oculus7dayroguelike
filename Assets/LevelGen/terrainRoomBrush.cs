using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
public class terrainRoomBrush : Brush {
	private GameObject self;
	public Material wallMaterial;
	public Material floorMaterial;
	public Material ceilingMaterial;
	private Dungeon map;
	private List<Vector3> connectionPoints = new List<Vector3>();

	public static terrainRoomBrush prepare(Dungeon map) {
		GameObject levelGen = GameObject.Find ("LevelGeneration");  //Is dumb, should fix
		terrainRoomBrush terrainRoomObject = levelGen.GetComponent<terrainRoomBrush> ();
		if (terrainRoomObject == null) {
			UnityEngine.Debug.Log("Please attach the terrainRoomBrush script to your LevelGeneration object.");
		}
		terrainRoomObject.map = map;
		terrainRoomObject.self = new GameObject ("terrainRoom");
		return terrainRoomObject;
	}

	public override void RenderRoom(Vector3 roomPosition, Vector3 roomSize) {
		Vector3[,,] roomPositions = new Vector3[(int)roomSize.x,(int)roomSize.y,(int)roomSize.z]; 
		
		for (int i = 0; i < roomSize.x; i++) {
			for (int j = 0; j < roomSize.y; j++) {
				for (int k = 0; k < roomSize.z; k++) {
					Transform wallPos;
					roomPositions[i,j,k] =  new Vector3 (roomPosition.x+i,roomPosition.y+j,roomPosition.z+k);
					// floor
//					if (!NeighborExists(roomPosition[i,j,k] + new Vector3 (0, -1, 0))) {
//						wallPos = newPlane (floorMaterial);
//						wallPos.Rotate(90,0,0);
//						wallPos.position = roomPosition[i,j,k] + new Vector3 (0, -0.5f, 0);
//					}
//					
//					// ceiling
//					if (!NeighborExists(roomPosition[i,j,k] + new Vector3 (0, 1, 0))) {
//						wallPos = newPlane (ceilingMaterial);
//						wallPos.Rotate(-90,0,0);
//						wallPos.position = roomPosition[i,j,k] + new Vector3 (0, 0.5f, 0);
//					}
//					
//					// walls
//					if (!NeighborExists(roomPosition[i,j,k] + new Vector3 (-1, 0, 0))) {
//						wallPos = newPlane (wallMaterial);
//						wallPos.Rotate(0,-90,0);
//						wallPos.position = roomPosition[i,j,k] + new Vector3 (-0.5f, 0, 0);
//					}
//					if (!NeighborExists(roomPosition[i,j,k] + new Vector3 (1, 0, 0))) {
//						wallPos = newPlane (wallMaterial);
//						wallPos.Rotate(0,90,0);
//						wallPos.position = roomPosition[i,j,k] + new Vector3 (0.5f, 0, 0);
//					}
//					if (!NeighborExists(roomPosition[i,j,k] + new Vector3 (0, 0, -1))) {
//						wallPos = newPlane (wallMaterial);
//						wallPos.Rotate(0,180,0);
//						wallPos.position = roomPosition[i,j,k] + new Vector3 (0, 0, -0.5f);
//					}
//					if (!NeighborExists(roomPosition[i,j,k] + new Vector3 (0, 0, 1))) {
//						wallPos = newPlane (wallMaterial);
//						wallPos.Rotate(0,0,0);
//						wallPos.position = roomPosition[i,j,k] + new Vector3 (0, 0, 0.5f);
//					}
				}
			}
		}


		GameObject terrain = GameObject.CreatePrimitive(PrimitiveType.Plane);
		GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
		terrain.transform.position = roomPosition;
		terrain.transform.localScale = roomSize;
		terrain.AddComponent ("TerrainToolkit");

		ceiling.transform.position = (roomPosition + new Vector3(0,5,0));
		ceiling.transform.Rotate(180,0,0);
		ceiling.transform.localScale = roomSize;
		
	}

	public List<Vector3> getConnectionPoints() {
		return connectionPoints;
	}

	public bool NeighborExists(Vector3 position) {
		// if out of bounds, no neighbor
		if (!map.inBounds(position)) {
			return false;
		}
		return map.getPosition(position).Count > 0;
	}

	private Transform newPlane (Material surfaceMaterial) {
		GameObject created = GameObject.CreatePrimitive (PrimitiveType.Quad);
		created.transform.parent = self.transform;
		created.renderer.material =  surfaceMaterial;
		return created.transform;
	}

//	void setRoomDimensions(Vector3 terrainSize_in) {
//		roomDimensions = terrainSize_in;
//		terrain.transform.localScale = terrainSize_in;
//	}
//
//	void setRoomPosition(Vector3 roomPosition_in) {
//		roomPosition = roomPosition_in;
//		terrain.transform.position = roomPosition_in;
//		ceiling.transform.position = (roomPosition_in + new Vector3(0,5,0));
//	}

	// Update is called once per frame
	void Update () {
		
	}
}*/
