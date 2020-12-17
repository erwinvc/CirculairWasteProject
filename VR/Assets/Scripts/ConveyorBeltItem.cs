using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltItem : MonoBehaviour {
    [NonSerialized] public Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
}
