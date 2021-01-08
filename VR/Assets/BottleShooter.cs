using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BottleShooter : MonoBehaviour {
    private SteamVR_Action_Boolean side = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    private SteamVR_Action_Boolean main = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Input_Sources input;
    public GameObject attachmentPoint;
    public GameObject bottle;

    private DateTime dt = DateTime.Now;
    void Update() {
        if ((DateTime.Now - dt).TotalMilliseconds > 100) {
            if (side.GetState(input)) {
                GameObject obj = Instantiate(bottle, transform.position + attachmentPoint.transform.forward * 0.4f, attachmentPoint.transform.rotation);
                obj.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(attachmentPoint.transform.forward * 1000);
                dt = DateTime.Now;
            }
        }
        if (main.GetStateDown(input)) {
            for (int y = -1; y <= 1; y++) {
                for (int x = -1; x <= 1; x++) {
                    GameObject obj = Instantiate(bottle,
                        transform.position + attachmentPoint.transform.forward * 0.4f +
                        (attachmentPoint.transform.right * ((float)x * 0.25f)) +
                        (attachmentPoint.transform.up * ((float)y * 0.25f)), attachmentPoint.transform.rotation);
                    obj.transform.GetChild(0).GetComponent<Rigidbody>()
                        .AddForce(attachmentPoint.transform.forward * 1000);
                }
            }
        }
    }
}
