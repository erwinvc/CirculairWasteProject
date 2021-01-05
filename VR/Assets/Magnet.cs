using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerStay(Collider other) {
        switch (other.tag) {
            case "WasteMetal":
            case "WasteGlass":
            case "WastePlastic":
            case "WastePaper": MagnetGameObject(other.gameObject); break;
        }
    }

    private void MagnetGameObject(GameObject obj) {
        obj.GetComponent<Rigidbody>().AddForce(Vector3.up);
    }
}
