using UnityEngine;
using System.Collections;

public class generateRoomWithTerrain : MonoBehaviour {

	public GameObject generatedTerrain;
	public Vector3 roomDimensions, roomPosition;

	// Use this for initialization
	generateRoomWithTerrain (Vector3 terrainSize_in, Vector3 roomPosition_in, ) {
		GameObject terrain = GameObject.CreatePrimitive(PrimitiveType.Plane);
		GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
		generatedTerrain.transform.position = roomPosition_in;
		generatedTerrain.transform.localScale = terrainSize_in;
	}

	void setRoomDimensions(Vector3 terrainSize_in) {
		roomDimensions = terrainSize_in;
		generatedTerrain.transform.localScale = terrainSize_in;
	}

	void setRoomPosition(Vector3 roomPosition_in) {
		roomPosition = roomPosition_in;
		generatedTerrain.transform.position = roomPosition_in;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
