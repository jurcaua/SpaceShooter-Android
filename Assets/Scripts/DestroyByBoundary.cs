using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        //Debug.Log("[" + other.attachedRigidbody.velocity.x + ", " + other.attachedRigidbody.velocity.y + ", " + other.attachedRigidbody.velocity.z + "]");
        Destroy(other.gameObject);
    }
}
