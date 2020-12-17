using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CustomTeleportArea : TeleportMarkerBase {
    Material material = null;


    public void Start() {
        if (material == null) {
            material = GetComponent<Renderer>().material;
        }
    }


    public override bool ShouldActivate(Vector3 playerPosition) {
        return true;
    }


    public override bool ShouldMovePlayer() {
        return true;
    }

    public override void WhileHighlight(Vector3 position) {
        if (material == null) {
            material = GetComponent<Renderer>().material;
        }
        material.SetVector("Point", position);
    }

    public override void Highlight(Vector3 position, bool highlight) {
        if (material == null) {
            material = GetComponent<Renderer>().material;
        }
        material.SetVector("Point", highlight ? position : new Vector3(int.MaxValue, int.MaxValue, int.MaxValue));
    }

    public override void SetAlpha(float tintAlpha, float alphaPercent) {
    }


    public override void UpdateVisuals() {

    }


    public void UpdateVisualsInEditor() {

    }


    private bool CalculateBounds() {
        //MeshFilter meshFilter = GetComponent<MeshFilter>();
        //if (meshFilter == null) {
        //    return false;
        //}
        //
        //Mesh mesh = meshFilter.sharedMesh;
        //if (mesh == null) {
        //    return false;
        //}
        //
        //meshBounds = mesh.bounds;
        return true;
    }


    //-------------------------------------------------
    private Color GetTintColor() {
        return Color.white;
        //if (locked) {
        //    return lockedTintColor;
        //} else {
        //    if (highlighted) {
        //        return highlightedTintColor;
        //    } else {
        //        return visibleTintColor;
        //    }
        //}
    }
}