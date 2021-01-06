using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCollider : MonoBehaviour {
    bool colliding = false;
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.rigidbody.tag);
        if (collision.rigidbody.tag == "WastePile") {
            print("yest");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "WastePile") {
            colliding = true;
        }
    }

    public bool IsColliding() {
        return colliding;
    }
}
