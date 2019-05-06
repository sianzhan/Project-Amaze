using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame

    void Update() 
    {
        if (MainController.CurrentGameState() == MainController.GameState.ENDGAME) 
        { 
            return;
        }
            //Fix camera position to player position
            Vector3 vecPos = player.transform.position;
        vecPos.y += 0.5f;
        transform.position = vecPos;

        //calculate rotation

        transform.localEulerAngles = CameraController.ViewRotation();

	}

    [ContextMenu("Test Run")]
    void TestRun()
    {
        Start();
        Update();
    }
}
