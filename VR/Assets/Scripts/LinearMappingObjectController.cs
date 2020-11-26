using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearMappingObjectController : MonoBehaviour {
    public LinearMapping mapVertical;
    public LinearMapping mapHorizontal;
    public LinearMapping mapUpDown;

    private Rigidbody rb;
    private Vector3 velocity = new Vector3();
    private float speed = 0.003f;

    public Bounds bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        velocity *= 0.95f;
        velocity += new Vector3((mapHorizontal.value - 0.5f) * speed, -(mapUpDown.value - 0.5f) * speed, (mapVertical.value - 0.5f) * speed);

        rb.MovePosition(transform.position +  velocity);

        if (transform.position.x > bounds.max.x) {
            transform.position = new Vector3(bounds.max.x, transform.position.y, transform.position.z);
            velocity.x = 0.0f;
        }

        if (transform.position.x < bounds.min.x) {
            transform.position = new Vector3(bounds.min.x, transform.position.y, transform.position.z);
            velocity.x = 0.0f;
        }

        if (transform.position.y < bounds.min.y) {
            transform.position = new Vector3(transform.position.x, bounds.min.y, transform.position.z);
            velocity.y = 0.0f;
        }

        if (transform.position.y > bounds.max.y) {
            transform.position = new Vector3(transform.position.x, bounds.max.y, transform.position.z);
            velocity.y = 0.0f;
        }

        if (transform.position.z > bounds.max.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, bounds.max.z);
            velocity.z = 0.0f;
        }

        if (transform.position.z < bounds.min.z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, bounds.min.z);
            velocity.z = 0.0f;
        }
    }
}
