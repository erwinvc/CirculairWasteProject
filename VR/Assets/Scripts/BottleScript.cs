using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour {
    public GameObject bottleBroken;
    public bool hard = false;
    private bool finished = false;
    private void OnCollisionEnter(Collision collision) {
        if (finished) return;
        float mag = collision.relativeVelocity.magnitude;
        if (collision.gameObject.tag == "Crusher" || (hard && mag > 4.0f)) {
            Rigidbody orb = GetComponent<Rigidbody>();
            GameObject brokenBottle = Instantiate(bottleBroken, transform.position, transform.rotation);
            foreach (Rigidbody rb in brokenBottle.GetComponentsInChildren<Rigidbody>()) {
                rb.velocity = orb.velocity + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                rb.angularVelocity = orb.angularVelocity + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            }
            Destroy(gameObject);
            finished = true;
        } else if (!hard) {
            if (mag > 1.5f && mag < 3.0f) {
                SoundEffectManager.SpawnTemporaryAudioSourceRandomIndex("GlassImpact", transform, true).Play();
            } else if (mag > 3.0f) {
                Rigidbody orb = GetComponent<Rigidbody>();
                GameObject brokenBottle = Instantiate(bottleBroken, transform.position, transform.rotation);
                foreach (Rigidbody rb in brokenBottle.GetComponentsInChildren<Rigidbody>()) {
                    rb.velocity = orb.velocity;
                    rb.angularVelocity = orb.angularVelocity;
                }

                SoundEffectManager.SpawnTemporaryAudioSourceRandomIndex("GlassShatter", brokenBottle.transform, true).Play();

                Destroy(brokenBottle, 6.0f);
                Destroy(gameObject);
                finished = true;
            }
        }
    }
}
