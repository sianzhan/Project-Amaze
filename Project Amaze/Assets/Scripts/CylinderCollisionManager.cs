using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCollisionManager : MonoBehaviour {

    public float boost = 1;
    public float maxBoost = 30;

    Vector3 boundsHalf;
	// Use this for initialization
	void Start () {
        boundsHalf = GetComponent<Collider>().bounds.size/2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb;
        if((rb = collision.collider.GetComponent<Rigidbody>()) != null)
        {
            Vector3 posCollider = collision.collider.transform.position;
            Vector3 posCylinder = transform.position;

            if (Mathf.Abs(posCollider.y - posCylinder.y) > boundsHalf.y - 1) return;
            posCollider.y = posCylinder.y = 0;
            if (Vector3.Distance(posCollider, posCylinder) > boundsHalf.x - 1) return;

            if (Mathf.Abs(rb.velocity.y) < maxBoost)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + boost, rb.velocity.z);
            }
        }
    }
}
