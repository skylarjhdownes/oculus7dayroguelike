using UnityEngine;

namespace OLDMyNameSpace {

	public class TestMapGen : MonoBehaviour {
		
		public static void Main() {
			
			Map nMap = MapGenerator.generateMapWithRectangularRoomsFirst(207, 207, .25, 999);
			
			nMap.printMap();
			
			Map M2 = MapGenerator.getMapWithRoomsFrom(nMap);
			MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom(M2,999);
			M2.printMap();
			
			
			Map M3 = MapGenerator.getMapWithRoomsFrom(nMap);
			MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom(M3,9999);
			M3.printMap();
			
			
			Map M4 = MapGenerator.getMapWithRoomsFrom(nMap);
			MapGenerator.buildRandomCorridorsFromAllRoomsToOneRoom(M4,99999);
			M4.printMap();
			
		}

		void Start() {
			Main ();
		}

		void update() {
		}
	}

}