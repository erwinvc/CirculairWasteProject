using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TestScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        foreach (var a in inputDevices) {
            print(a.name);
        }

        XRController controller = GetComponent<XRController>();
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool menuButton)) {
            print("Menu button: " + menuButton);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
