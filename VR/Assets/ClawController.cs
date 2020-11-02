using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClawController : MonoBehaviour {
    private List<GameObject> pivots = new List<GameObject>();
    public LinearMapping linearMapping;

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name.StartsWith("ClawPivot")) {
                pivots.Add(child);
                Vector3 rot = child.transform.localRotation.eulerAngles;
                rot.z = 40;
                child.transform.localRotation = Quaternion.Euler(rot);
            }
        }
    }

    bool a = false;
    private float value = 0.0f;
    void Update() {
        value = linearMapping.value * 50;
        //if (a) {
        //    value += 20 * Time.deltaTime;
        //    if(value > 50.0f) a = false;
        //} else {
        //    value -= 20 * Time.deltaTime;
        //    if (value < 0.0f) a = true;
        //}

        foreach (var pivot in pivots) {
            Vector3 rot = pivot.transform.localRotation.eulerAngles;
            rot.z = value;
            pivot.transform.localRotation = Quaternion.Euler(rot);
        }
    }
}
