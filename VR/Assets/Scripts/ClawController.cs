using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClawController : MonoBehaviour {
    private List<Rigidbody> rbs = new List<Rigidbody>();
    private List<HingeJoint> joints = new List<HingeJoint>();
    public LinearMapping openCloseLinearMapping;
    

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name.StartsWith("ClawPivot")) {
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

    void FixedUpdate() {
        foreach (var joint in joints) {
            JointSpring spring = joint.spring;
            spring.targetPosition = (1.0f-openCloseLinearMapping.value) * 50.0f;
            joint.spring = spring;
        }
    }
}
