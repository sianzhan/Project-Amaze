using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour {

    
    public GameObject player;
    public float size = 20;

    public float offsetHeight = 10;

	// Use this for initialization
	void Start () {
        SetSize(size);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 posPlayer = player.transform.position;
        posPlayer.y += offsetHeight;
        transform.position = posPlayer;
    }

    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }
}
