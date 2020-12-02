using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WasteMove : MonoBehaviour {
    public enum WasteType {
        NONE,
        METAL,
        GLASS,
        PLASTIC,
        PAPER
    }

    public WasteType wasteType;
    public Teleport teleport;
    public TeleportPoint playerTeleportPoint;
    private GameObject teleportPoint;

    void Start() {
        teleportPoint = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other) {
        if (playerTeleportPoint == teleport.currentTeleportPoint) return;
        if (TagToWasteType(other.tag) == wasteType) {
            Vector3 pos = Vector3.zero;
            Offset offset = other.GetComponent<Offset>();
            if (offset) {
                pos = offset.offset;
            }
            other.transform.position = teleportPoint.transform.position - pos;
            other.transform.rotation = Quaternion.identity;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private WasteType TagToWasteType(string tag) {
        switch (tag) {
            case "WasteMetal": return WasteType.METAL;
            case "WasteGlass": return WasteType.GLASS;
            case "WastePlastic": return WasteType.PLASTIC;
            case "WastePaper": return WasteType.PAPER;
        }

        return WasteType.NONE;
    }
}
