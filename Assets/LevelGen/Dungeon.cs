using UnityEngine;
using System.Collections.Generic;
namespace LevelGen {
	public class Dungeon {
		private Dictionary<Position, List<Brush>> map;
		private GameObject parent;
		private List<GameObject> children = new List<GameObject>();

		public Dungeon() {
			map = new Dictionary<Position, List<Brush>>();

			parent = new GameObject ("GeneratedDungeon");
		}

		public void RenderAll() {
			foreach (var pair in map) {
				foreach (var brush in pair.Value) {
					brush.RenderRoom(pair.Key, this);
				}
			}
			parent.transform.localScale = Scaled (parent.transform.localScale);
		}

		public void Place(Position position, Brush brush) {
			if(!map.ContainsKey(position))
				map [position] = new List<Brush>();
			map[position].Add (brush);
		}

		public bool HasContent(Position position) {
			return map.ContainsKey (position);
		}

		public void AddChild(GameObject child) {
			children.Add(child);
			child.transform.parent = parent.transform;
		}

		public void Delete() {
			foreach (var child in children)
				Object.Destroy (child);
			children.Clear();
			Object.Destroy (parent);
		}

		private Vector3 scale = new Vector3 (2, 3, 2);
		public Vector3 Scaled(Vector3 un) {
			return new Vector3 (un.x * scale.x, un.y * scale.y, un.z * scale.z);
		}
	}
}