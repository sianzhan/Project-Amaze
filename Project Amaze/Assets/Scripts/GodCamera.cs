using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCamera : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (MainController.CurrentGameState() == MainController.GameState.ENDGAME) {
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 2);
        }
        else if (enabled)
        {
            Vector3 movMouse = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            movMouse += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
            transform.RotateAround(Vector3.zero, Vector3.up, movMouse.x);
        }


	}
}
