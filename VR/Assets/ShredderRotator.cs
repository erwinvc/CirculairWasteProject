using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderRotator : MonoBehaviour {
    public bool flipped = false;

    void FixedUpdate() {
        transform.Rotate(0.0f, flipped ? 20f : -2.0f, 0.0f, Space.Self);
    }
}
