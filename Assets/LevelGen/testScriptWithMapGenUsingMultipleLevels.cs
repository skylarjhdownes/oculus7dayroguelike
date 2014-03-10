
using UnityEngine;
using System.Collections;
using MyNameSpace;

public class testScriptWithMapGenUsingMultipleLevels : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {

		Map[] mList = new Map [5];

		mList[0] = MapGenerator.generateMapWithRectangularRoomsFirst (60, 60, .15, 251);
		mList[1] = MapGenerator.getMapWithRoomsFrom (mList[0]);
		mList[2] = MapGenerator.getMapWithRoomsFrom (mList[0]);
		mList[3] = MapGenerator.getMapWithRoomsFrom (mList[0]);
		mList[4] = MapGenerator.getMapWithRoomsFrom (mList[0]);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (mList[1], 1252);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (mList[2], 9252);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (mList[3], 90252);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (mList[4], 9251);

		d = new dungeonMap (mList[0].width, mList.Length, mList[0].height);
		hallways h = hallways.prepare (d);

		for (int k = 0; k < mList.Length; k++) {
			for (int i = 0; i < mList[0].width; i++) {
				for (int j = 0; j < mList[0].height; j++) {
					switch (mList[k].MapGrid[i,j]) {
					case 0:
					case 2:
						d.place (i,k,j, h);
						break;
					}
				}
			}
		}


		d.RenderAll ();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
