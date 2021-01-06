using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour {
    public GameObject bottleBroken;
    private void OnCollisionEnter(Collision collision) {
        float mag = collision.relativeVelocity.magnitude;
        if (mag > 1.5f && mag < 3.0f) {
            SoundEffectManager.SpawnTemporaryAudioSourceRandomIndex("GlassImpact", transform, true).Play();
        } else if (collision.relativeVelocity.magnitude > 3.0f) {
            Rigidbody orb = GetComponent<Rigidbody>();
            GameObject brokenBottle = Instantiate(bottleBroken, transform.position, transform.rotation);
            foreach (Rigidbody rb in brokenBottle.GetComponentsInChildren<Rigidbody>()) {
                rb.velocity = orb.velocity;
                rb.angularVelocity = orb.angularVelocity;
            }

            SoundEffectManager.SpawnTemporaryAudioSourceRandomIndex("GlassShatter", brokenBottle.transform, true).Play();

            Destroy(brokenBottle, 6.0f);
            Destroy(gameObject);
        }
    }
}
