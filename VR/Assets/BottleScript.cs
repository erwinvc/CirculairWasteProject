using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour {
    public GameObject bottleBroken;

    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 3.0f) {
            Rigidbody orb = GetComponent<Rigidbody>();
            GameObject brokenBottle = Instantiate(bottleBroken, transform.position, transform.rotation);
            foreach (Rigidbody rb in brokenBottle.GetComponentsInChildren<Rigidbody>()) {
                rb.velocity = orb.velocity;
                rb.angularVelocity = orb.angularVelocity;
            }
            Destroy(gameObject);
        }
    }
}
