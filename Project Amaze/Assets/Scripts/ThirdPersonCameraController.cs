using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {

    public GameObject player;
    public float distanceFromPlayer;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    Vector3 vecPlayerCamera;


    void Start()
    {

    }

    // Update is called once per frame

    // Update is called once per frame
    void Update () {
        //Calculate Player Camera Distance
        vecPlayerCamera = -(transform.rotation * Vector3.forward) * distanceFromPlayer;
        vecPlayerCamera += new Vector3(offsetX, offsetY, offsetZ);
        Vector3 posPlayer = player.transform.position;
        Vector3 posCamera = posPlayer + vecPlayerCamera;

        transform.position = posCamera;

        transform.localEulerAngles = CameraController.ViewRotation();
    }

    [ContextMenu("Test Run")]
    void TestRun()
    {
        Start();
        Update();
    }
}
