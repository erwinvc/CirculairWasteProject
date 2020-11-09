using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClawController : MonoBehaviour {
    //private List<GameObject> pivots = new List<GameObject>();
    private List<Rigidbody> rbs = new List<Rigidbody>();
    private List<HingeJoint> joints = new List<HingeJoint>();
    public LinearMapping openCloseLinearMapping;

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name.StartsWith("ClawPivot")) {
                //pivots.Add(child);
                HingeJoint jnt = child.GetComponent<HingeJoint>();
                JointSpring spring = jnt.spring;
                spring.spring = 1000;
                spring.damper = 100;
                spring.targetPosition = openCloseLinearMapping.value * 50.0f;
                jnt.spring = spring;
                jnt.useSpring = true;
                joints.Add(jnt);
                rbs.Add(child.GetComponent<Rigidbody>());
                Vector3 rot = child.transform.localRotation.eulerAngles;
                rot.z = 40;
                child.transform.localRotation = Quaternion.Euler(rot);
            }
        }
    }

    bool a = false;
    private float value = 0.0f;

    void FixedUpdate() {
        value = openCloseLinearMapping.value * 50;
        //if (a) {
        //    value += 20 * Time.deltaTime;
        //    if(value > 50.0f) a = false;
        //} else {
        //    value -= 20 * Time.deltaTime;
        //    if (value < 0.0f) a = true;
        //}

        //foreach (var pivot in pivots) {
        //    Vector3 rot = pivot.transform.localRotation.eulerAngles;
        //    rot.z = value;
        //    pivot.transform.localRotation = Quaternion.Euler(rot);
        //}

        foreach (var joint in joints) {
            JointSpring spring = joint.spring;
            spring.targetPosition = (1.0f-openCloseLinearMapping.value) * 50.0f;
            joint.spring = spring;
        }

        //foreach (var rb in rbs)
        //{
        //    Vector3 rot = rb.transform.localRotation.eulerAngles;
        //    rot.z = value;
        //    rb.MoveRotation(Quaternion.Euler(rot));
        //    joints[0].spring
        //}
    }
}
