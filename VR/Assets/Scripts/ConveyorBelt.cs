using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    public float speed = 0.1f;

    private Rigidbody rb;
    private Vector3 originalPosition;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
    }

    void FixedUpdate() {
        rb.position += (transform.right * Time.fixedDeltaTime * speed);
        rb.MovePosition(originalPosition);
    }
}


