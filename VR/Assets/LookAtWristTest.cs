using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAtWristTest : MonoBehaviour {
    public GameObject head;
    public GameObject attachmentPoint;
    public GameObject sphere;
    public GameObject tablet;

    private bool armCurrentlyVisible = false;
    private float lookingAtArmValue = -0.95f;
    private float lookingAwayFromArmValue = -0.75f;

    private void Start()
    {
        tablet.SetActive(false);
    }


    void FixedUpdate() {
        Vector3 armVector = -attachmentPoint.transform.right;
        Vector3 headToArmVector = (attachmentPoint.transform.position - head.transform.position).normalized;
        DebugLineRenderer.Draw(attachmentPoint.transform.position, attachmentPoint.transform.position + armVector, armCurrentlyVisible ? Color.green : Color.red);
        DebugLineRenderer.Draw(attachmentPoint.transform.position, attachmentPoint.transform.position + headToArmVector, Color.blue);

        if (armCurrentlyVisible) {
            if (Vector3.Dot(armVector, headToArmVector) <= lookingAwayFromArmValue && Vector3.Dot(head.transform.forward, -headToArmVector) <= lookingAwayFromArmValue) return;
            tablet.SetActive(false);
            armCurrentlyVisible = false;
        } else {
            if (Vector3.Dot(armVector, headToArmVector) >= lookingAtArmValue || Vector3.Dot(head.transform.forward, -headToArmVector) >= lookingAtArmValue) return;
            tablet.SetActive(true);
            armCurrentlyVisible = true;
            
        }
    }
}
