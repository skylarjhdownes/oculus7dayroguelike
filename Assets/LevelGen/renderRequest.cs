using UnityEngine;
using System.Collections;

public class renderRequest {
	private Vector3 size;
	private brush brush;
	
	public renderRequest(Vector3 size, brush brush) {
		this.size = size;
		this.brush = brush;
	}
	
	public void render(Vector3 position) {
		brush.RenderRoom (position, size);
	}
}