using UnityEngine;
using System.Collections;
using Assets;

public class TextureOffset: MonoBehaviour {
	public float scrollSpeed = 0.5F;
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
        Global glb = Global.getInstance();
        float offset = 0.05f * Mathf.Sin(Time.time * glb._speed * 20) + 0.05f;
		rend.material.SetTextureOffset("_MainTex", new Vector2(0,offset));
	}
}