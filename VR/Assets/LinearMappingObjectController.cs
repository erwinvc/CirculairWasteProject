using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearMappingObjectController : MonoBehaviour {
    public LinearMapping mapVertical;
    public LinearMapping mapHorizontal;

    private Vector3 velocity = new Vector3();
    void FixedUpdate() {
        velocity *= 0.95f;
        velocity -= new Vector3((mapHorizontal.value - 0.5f) * 0.003f, 0, (mapVertical.value - 0.5f) * 0.003f);
        transform.position += velocity;
        if (transform.position.z < 3.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 3.0f);
            velocity.z = 0.0f;
        }
    }
}
