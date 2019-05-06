using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {

    public GameObject refObj;

    RectTransform rectParent;
    Camera cam;
	// Use this for initialization
	void Start () {
        rectParent = transform.parent.GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = Camera.main.WorldToScreenPoint(refObj.transform.position);
        pos.z = 0;

        if (pos.y > Screen.height) pos.y = Screen.height - 10;

        pos.x = pos.x * rectParent.rect.width / Screen.width - rectParent.rect.width/2;
        pos.y = pos.y * rectParent.rect.height / Screen.height - rectParent.rect.height/2;


        transform.localPosition = pos;
    }
}
