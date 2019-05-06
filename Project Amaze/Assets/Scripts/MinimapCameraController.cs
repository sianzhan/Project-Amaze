using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour {

    public GameObject player;
    public float Height = 500;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        Vector3 posPlayer = player.transform.position;
        posPlayer.y = Height;
        transform.position = posPlayer;


        //Fix camera position to player position
        /*
        Vector3 vecPlayerCamera = transform.position - player.transform.position;
        vecPlayerCamera.y = 0;

        vecPlayerCamera = Vector3.ClampMagnitude(vecPlayerCamera, 10);
        vecPlayerCamera.y = offsetHeight;
               

        transform.position = player.transform.position + vecPlayerCamera;
        */
        //calculate rotation

    }

    [ContextMenu("Test Run")]
    void TestRun()
    {
        Start();
        Update();
    }
}
