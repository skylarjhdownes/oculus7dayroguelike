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
	}
}