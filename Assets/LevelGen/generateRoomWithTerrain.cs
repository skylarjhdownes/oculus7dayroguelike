using UnityEngine;
using System.Collections;

public class generateRoomWithTerrain : room {
	private GameObject self;
	public Texture wallTexture;
	public Texture floorTexture;
	public Texture ceilingTexture;
	private dungeonMap map;


	public static generateRoomWithTerrain prepare(dungeonMap map) {
		GameObject levelGen = GameObject.Find ("LevelGeneration");  //Is dumb, should fix
		generateRoomWithTerrain terrainRoomObject = levelGen.GetComponent<generateRoomWithTerrain> ();
		terrainRoomObject.map = map;
		terrainRoomObject.self = new GameObject ("terrainRoom");
		return terrainRoomObject;
	}

	public override void RenderRoom(Vector3 roomPosition, Vector3 roomSize) {
		GameObject terrain = GameObject.CreatePrimitive(PrimitiveType.Plane);
		GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
		terrain.transform.position = roomPosition;
		terrain.transform.localScale = roomSize;
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
