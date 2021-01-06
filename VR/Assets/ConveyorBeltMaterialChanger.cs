using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltMaterialChanger : MonoBehaviour {
    public Material newMaterial;

    public void Activate() {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material[] mats = renderer.materials;
        mats[0] = newMaterial;
        renderer.materials = mats;
    }
}
