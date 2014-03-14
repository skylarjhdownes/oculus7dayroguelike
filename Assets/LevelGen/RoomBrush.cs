using UnityEngine;
using System.Collections.Generic;
using System;
namespace LevelGen {
public class RoomBrushFactory {
	private readonly List<string> wallMaterialSources = new List<string> 
	{
		"Grass", "Cobble pave", "Forest floor"
	};
	private readonly List<string> floorMaterialSources = new List<string> 
	{
		"Grass", "Cobble pave", "Forest floor"
	};
	private readonly List<string> ceilingMaterialSources = new List<string> 
	{
		"Grass", "Cobble pave", "Forest floor"
	};

	private readonly List<Material> wallMaterials;
	private readonly List<Material> floorMaterials;
	private readonly List<Material> ceilingMaterials;

	public RoomBrushFactory ()
	{
		wallMaterials = loadAll (wallMaterialSources);
		floorMaterials = loadAll (floorMaterialSources);
		ceilingMaterials = loadAll (ceilingMaterialSources);
	}

	private List<Material> loadAll(List<string> resources)
	{
		List<Material> materials = new List<Material> ();
		foreach (string resource in resources) {
			Material material = Resources.Load<Material> (resource);
			if (material != null) {
				materials.Add (material);
			} else {
				UnityEngine.Debug.LogWarning ("Unable to load material at " + resource);
			}
		}
		return materials;
	}

	private Material pick(List<Material> list, System.Random rng) {
		return list [rng.Next (list.Count)];
	}

	public RoomBrush createRoomBrush(System.Random rng) {
		return new RoomBrush (pick (wallMaterials, rng), pick (floorMaterials, rng), pick (ceilingMaterials, rng));
	}
}

public class RoomBrush : Brush {
	private Material wallMaterial;
	private Material floorMaterial;
	private Material ceilingMaterial;

	public RoomBrush (Material wallMaterial, Material floorMaterial, Material ceilingMaterial)
	{
		this.wallMaterial = wallMaterial;
		this.floorMaterial = floorMaterial;
		this.ceilingMaterial = ceilingMaterial;
	}
	
	public void RenderRoom(Position pos, Dungeon map) {
		GameObject wall;

		Debug.Log (map.HasContent(pos + new Position (0, -1, 0)));

		// floor
		if (!map.HasContent(pos + new Position (0, -1, 0))) {
			wall = GameObject.CreatePrimitive (PrimitiveType.Quad);
			wall.renderer.material = floorMaterial;
			map.AddChild(wall);
			wall.transform.Rotate(90,0,0);
			wall.transform.position = pos.Vector3 + new Vector3 (0, -0.5f, 0);
		}
		
		// ceiling
		if (!map.HasContent(pos + new Position (0, 1, 0))) {
			wall = GameObject.CreatePrimitive (PrimitiveType.Quad);
			wall.renderer.material = ceilingMaterial;
			map.AddChild(wall);
			wall.transform.Rotate(-90,0,0);
			wall.transform.position = pos.Vector3 + new Vector3 (0, 0.5f, 0);
		}
		
		// walls
		if (!map.HasContent(pos + new Position (-1, 0, 0))) {
			wall = GameObject.CreatePrimitive (PrimitiveType.Quad);
			wall.renderer.material = wallMaterial;
			map.AddChild(wall);
			wall.transform.Rotate(0,-90,0);
			wall.transform.position = pos.Vector3 + new Vector3 (-0.5f, 0, 0);
		}
		if (!map.HasContent(pos + new Position (1, 0, 0))) {
			wall = GameObject.CreatePrimitive (PrimitiveType.Quad);
			wall.renderer.material = wallMaterial;
			map.AddChild(wall);
			wall.transform.Rotate(0,90,0);
			wall.transform.position = pos.Vector3 + new Vector3 (0.5f, 0, 0);
		}
		if (!map.HasContent(pos + new Position (0, 0, -1))) {
			wall = GameObject.CreatePrimitive (PrimitiveType.Quad);
			wall.renderer.material = wallMaterial;
			map.AddChild(wall);
			wall.transform.Rotate(0,180,0);
			wall.transform.position = pos.Vector3 + new Vector3 (0, 0, -0.5f);
		}
		if (!map.HasContent(pos + new Position (0, 0, 1))) {
			wall = GameObject.CreatePrimitive (PrimitiveType.Quad);
			wall.renderer.material = wallMaterial;
			map.AddChild(wall);
			wall.transform.Rotate(0,0,0);
			wall.transform.position = pos.Vector3 + new Vector3 (0, 0, 0.5f);
		}
	}
}
}