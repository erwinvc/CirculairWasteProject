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
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        velocity *= 0.95f;
        velocity -= new Vector3((mapHorizontal.value - 0.5f) * speed, (mapUpDown.value - 0.5f) * speed, (mapVertical.value - 0.5f) * speed);

        rb.MovePosition(transform.position +  velocity);

        if (transform.position.x > 4.5f) {
            transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
            velocity.x = 0.0f;
        }

        if (transform.position.x < -4.5f) {
            transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
            velocity.x = 0.0f;
        }

        if (transform.position.y < 1.8f) {
            transform.position = new Vector3(transform.position.x, 1.8f, transform.position.z);
            velocity.y = 0.0f;
        }

        if (transform.position.y > 10.0f) {
            transform.position = new Vector3(transform.position.x, 10.0f, transform.position.z);
            velocity.y = 0.0f;
        }

        if (transform.position.z > 11.0f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 11.0f);
            velocity.z = 0.0f;
        }

        if (transform.position.z < 3.0f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 3.0f);
            velocity.z = 0.0f;
        }

        if (transform.position.z > 11.0f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 11.0f);
            velocity.z = 0.0f;
        }
    }
}
