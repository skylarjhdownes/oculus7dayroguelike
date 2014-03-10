using UnityEngine;
using System.Collections;

public class generateRoomWithTerrain : MonoBehaviour {

	public GameObject terrain, ceiling;
	public Vector3 roomDimensions, roomPosition;

	// Use this for initialization
	generateRoomWithTerrain (Vector3 roomDimensions_in, Vector3 roomPosition_in) {
		terrain = GameObject.CreatePrimitive(PrimitiveType.Plane);
		ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
		terrain.transform.position = roomPosition_in;
		terrain.transform.localScale = roomDimensions_in;
		ceiling.transform.position = (roomPosition_in + new Vector3(0,5,0));
		ceiling.transform.localScale = roomDimensions_in;
	}

	void setRoomDimensions(Vector3 terrainSize_in) {
		roomDimensions = terrainSize_in;
		terrain.transform.localScale = terrainSize_in;
	}

	void setRoomPosition(Vector3 roomPosition_in) {
		roomPosition = roomPosition_in;
		terrain.transform.position = roomPosition_in;
		ceiling.transform.position = (roomPosition_in + new Vector3(0,5,0));
	}

	// Update is called once per frame
	void Update () {
		
	}
}
