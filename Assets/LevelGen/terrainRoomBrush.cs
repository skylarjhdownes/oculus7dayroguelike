using UnityEngine;
using System.Collections;

public class terrainRoomBrush : paintRoom {
	private GameObject self;
	public Material wallMaterial;
	public Material floorMaterial;
	public Material ceilingMaterial;
	private dungeonMap map;


	public static terrainRoomBrush prepare(dungeonMap map) {
		GameObject levelGen = GameObject.Find ("LevelGeneration");  //Is dumb, should fix
		terrainRoomBrush terrainRoomObject = levelGen.GetComponent<terrainRoomBrush> ();
		if (terrainRoomObject == null) {
			UnityEngine.Debug.Log("Please attach the generateRoomWithTerrain object to your LevelGeneration object.");
		}
		terrainRoomObject.map = map;
		terrainRoomObject.self = new GameObject ("terrainRoom");
		return terrainRoomObject;
	}

	public override void RenderRoom(Vector3 roomPosition, Vector3 roomSize) {
		GameObject terrain = GameObject.CreatePrimitive(PrimitiveType.Plane);
		GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
		terrain.transform.position = roomPosition;
		terrain.transform.localScale = roomSize;
		terrain.AddComponent ("TerrainToolkit");

		ceiling.transform.position = (roomPosition + new Vector3(0,5,0));
		ceiling.transform.Rotate(180,0,0);
		ceiling.transform.localScale = roomSize;
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
}
