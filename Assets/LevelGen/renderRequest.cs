using UnityEngine;
using System.Collections;

public class renderRequest {
	private Vector3 size;
	private paintRoom brush;
	
	public renderRequest(Vector3 size, paintRoom brush) {
		this.size = size;
		this.brush = brush;
	}
	
	public void render(Vector3 position) {
		brush.RenderRoom (position, size);
	}
}