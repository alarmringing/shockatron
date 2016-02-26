using UnityEngine;
using System.Collections;

public class wire_render : MonoBehaviour {

	void OnPreRender() {
		GL.wireframe = true;
	}
	void OnPostRender() {
		GL.wireframe = false;
	}
}
