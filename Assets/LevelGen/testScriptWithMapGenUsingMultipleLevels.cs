
using UnityEngine;
using System.Collections;
using MyNameSpace;

[ExecuteInEditMode]
public class testScriptWithMapGenUsingMultipleLevels : MonoBehaviour {
	private dungeonMap d;

	// Use this for initialization
	void Start() {
		Map p = MapGenerator.generateMapWithRectangularRoomsFirst (60, 60, .15, 251);

		d = new dungeonMap (p.width, 5, p.height);
		room e = null;
		hallways h = hallways.prepare (d);
		d.map = new room[p.width, 5, p.height];


		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p.MapGrid[i,j]) {
				case 0:
				case 2:
					d.map[i,0,j] = h;
					break;
					
				case 99:
					d.map[i,0,j] = e;
					break;
					
				}
			}
		}

		Map p2 = MapGenerator.getMapWithRoomsFrom (p);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (p2, 252);
		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p2.MapGrid[i,j]) {
				case 0:
				case 2:
					d.map[i,1,j] = h;
					break;
					
				case 99:
					d.map[i,1,j] = e;
					break;
					
				}
			}
		}

		Map p3 = MapGenerator.getMapWithRoomsFrom (p);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (p3, 1252);
		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p3.MapGrid[i,j]) {
				case 0:
				case 2:
					d.map[i,2,j] = h;
					break;
					
				case 99:
					d.map[i,2,j] = e;
					break;
					
				}
			}
		}

		/*
		Map p4 = MapGenerator.getMapWithRoomsFrom (p);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (p4, 10252);
		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p4.MapGrid[i,j]) {
				case 0:
				case 2:
					d.map[i,3,j] = h;
					break;
					
				case 99:
					d.map[i,3,j] = e;
					break;
					
				}
			}
		}

		Map p5 = MapGenerator.getMapWithRoomsFrom (p);
		MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom (p5, 12529);
		for (int i = 0; i < p.width; i++) {
			for (int j = 0; j < p.height; j++) {
				switch (p5.MapGrid[i,j]) {
				case 0:
				case 2:
					d.map[i,4,j] = h;
					break;
					
				case 99:
					d.map[i,4,j] = e;
					break;
					
				}
			}
		}
		*/

		
		
		d.RenderAll ();
	}


	// Update is called once per frame
	void Update () {
		
	}
}
