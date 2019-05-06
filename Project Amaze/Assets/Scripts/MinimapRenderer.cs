using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRenderer : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = transform.localEulerAngles;
        rotation.z = CameraController.ViewRotation().y;

        transform.localEulerAngles = rotation;
    }
}
