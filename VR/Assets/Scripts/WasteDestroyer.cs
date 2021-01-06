using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteDestroyer : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {

        switch (other.tag) {
            case "WasteMetal":
            case "WasteIron":
            case "WasteGlass":
            case "WastePlastic":
            case "WasteBale":
            case "WastePaper": Destroy(other.gameObject, 0.5f); break;
        }
    }
}
