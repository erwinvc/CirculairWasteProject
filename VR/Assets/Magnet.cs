using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
    private List<Rigidbody> rbs = new List<Rigidbody>();

    public bool working = false;

    public void SetWorking(bool toggle) {
        working = toggle;
        MagnetTask.Finished = true;
    }
    private void FixedUpdate() {
        if (working) {
            foreach (Rigidbody rb in rbs) {
                rb.AddForce(Vector3.up * 25);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "WasteIron") {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb && !rbs.Contains(rb)) rbs.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "WasteIron") {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) rbs.Remove(rb);
        }
    }
}
