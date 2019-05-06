using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime * 60, Space.World);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false); 
        }

    }
}
