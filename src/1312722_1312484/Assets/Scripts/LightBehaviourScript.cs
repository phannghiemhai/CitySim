using UnityEngine;
using System.Collections;
using Assets;

public class LightBehaviourScript : MonoBehaviour {
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        Global glb = Global.getInstance();
        float xSpeed = glb._speed * 10;
        float ySpeed = 0;
        float zSpeed = glb._speed * 10;

        transform.Rotate(
            xSpeed * Time.deltaTime,
            ySpeed * Time.deltaTime,
            zSpeed * Time.deltaTime
       );
    }
}
