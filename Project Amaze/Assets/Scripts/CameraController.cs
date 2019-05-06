using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraFirstPerson;
    public GameObject cameraThirdPerson;
    public GameObject cameraGod;

    private static int cameraActive;

    List<GameObject> cameras;

    // Use this for initialization
    void Start()
    {
        cameras = new List<GameObject>();
        cameras.Add(cameraFirstPerson);
        cameras.Add(cameraThirdPerson);
        cameras.Add(cameraGod);
        for (int i = 0; i < cameras.Count; ++i) cameras[i].GetComponent<Camera>().enabled = false;

        cameraFirstPerson.GetComponent<Camera>().enabled = true;
        cameraActive = 0;

    }

    // Update is called once per frame
    int preActiveCamera = 0;
    void Update()
    {


        if(MainController.CurrentGameState() == MainController.GameState.PAUSE)
        {
            return;
        }else if(MainController.CurrentGameState() == MainController.GameState.ENDGAME)
        {
            if (cameraActive != 0)
            {
                ActiveCamera(0);
            }

            cameraFirstPerson.transform.position =
            Vector3.Lerp(cameraFirstPerson.transform.position, cameraGod.transform.position, 0.007f);

            cameraFirstPerson.transform.rotation =
            Quaternion.Lerp(cameraFirstPerson.transform.rotation, cameraGod.transform.rotation, 0.01f);

            cameraFirstPerson.GetComponent<Camera>().fieldOfView = 
           Mathf.Lerp(cameraFirstPerson.GetComponent<Camera>().fieldOfView, cameraGod.GetComponent<Camera>().fieldOfView, 0.01f);

            return;
        }
        UpdateViewRotation();
        if (Input.GetKeyDown(KeyCode.C))
        {
            ActiveCamera(preActiveCamera = ((cameraActive + 1) % 2));

        }else if(Input.GetKeyDown(KeyCode.G))
        {
            if (cameraActive == 2) ActiveCamera(preActiveCamera);
            else
            {
                preActiveCamera = cameraActive;
                ActiveCamera(2);
            }
        }
    }

    void ActiveCamera(int indexCamera)
    {
        if (Camera.main.gameObject != cameras[indexCamera])
        {
            foreach (GameObject kam in cameras)
            {
                if (kam == cameras[indexCamera])
                {
                    kam.GetComponent<Camera>().enabled = true;
                }
                else
                {
                    kam.GetComponent<Camera>().enabled = false;
                }
            }

            cameraActive = indexCamera;
        }
    }

    static Vector3 rotation = new Vector3(0, 0, 0);
    public float speedRotate; 
    static float minAngleVertical = -80f;
    static float maxAngleVertical = 80f;


    private void UpdateViewRotation()
    {
        if (cameraActive != 2)
        {
            Vector3 movMouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * speedRotate;
            rotation.x -= movMouse.y;
            if (rotation.x < 0f) rotation.x += 360f;
            else if (rotation.x > 360f) rotation.x -= 360f;
            if (rotation.x > 180f)
            {
                if (rotation.x < (360f + minAngleVertical)) rotation.x = (360f + minAngleVertical);
            }
            else if (rotation.x > maxAngleVertical) rotation.x = maxAngleVertical;

            rotation.y += movMouse.x;
            rotation.z = 0;
        }
    }

    public static Vector3 ViewRotation()
    {
        return rotation;
    }

    public static int ActiveCamera()
    {
        return cameraActive;
    }

}
