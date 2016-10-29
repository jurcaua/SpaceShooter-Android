using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
    
	void Update()
    {
        if (transform.position.z <= -16)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 25);
        }
    }
}
