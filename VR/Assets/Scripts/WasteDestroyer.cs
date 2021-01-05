using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteDestroyer : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {

        switch (other.tag) {
            case "WasteMetal":
            case "WasteGlass":
            case "WastePlastic":
            case "WastePaper": Destroy(other.gameObject); break;
        }
    }
}
