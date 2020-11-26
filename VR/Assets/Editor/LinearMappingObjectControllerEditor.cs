using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


[CanEditMultipleObjects]
[CustomEditor(typeof(LinearMappingObjectController), true)]
public class LinearMappingObjectControllerEditor : Editor {
    SerializedProperty bounds;
    BoxBoundsHandle boxBounds;

    void OnEnable() {
        bounds = serializedObject.FindProperty("bounds");
        boxBounds = new BoxBoundsHandle();
    }

    //[DrawGizmo(GizmoType.Active)]
    //static void DrawHandles(LinearMappingObjectController lmoc, GizmoType gizmoType) {
    //    GUIStyle customGUIStyle = new GUIStyle(GUI.skin.button);
    //    customGUIStyle.normal.textColor = Color.black;
    //    customGUIStyle.fontSize = 10;
    //    customGUIStyle.alignment = TextAnchor.MiddleCenter;
    //    GUI.backgroundColor = new Color(0.75f, 1.0f, 0.75f, 0.95f);
    //    Handles.Label(lmoc.transform.position, lmoc.name, customGUIStyle);
    //
    //    //Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
    //    //Handles.CircleHandleCap(0, lmoc.transform.position, Quaternion.identity, lmoc.audioSource.maxDistance, EventType.Repaint);
    //    //Handles.CircleHandleCap(0, lmoc.transform.position, Quaternion.identity, lmoc.audioSource.minDistance, EventType.Repaint);
    //
    //    //if ((gizmoType & GizmoType.NotInSelectionHierarchy) != 0) {
    //        Gizmos.DrawWireCube(lmoc.transform.position + lmoc.bounds.center, lmoc.bounds.size);
    //        Gizmos.color = new Color(0.75f, 1.0f, 0.75f, 0.1f);
    //        Gizmos.DrawCube(lmoc.bounds.center, lmoc.bounds.size);
    //    //}
    //}

    public void OnSceneGUI() {
        LinearMappingObjectController targ = (LinearMappingObjectController)target;
        var localToWorld = Matrix4x4.TRS(targ.transform.position, targ.transform.rotation, Vector3.one);
        //using (new Handles.DrawingScope(Color.white, localToWorld)) {
            boxBounds.center = targ.bounds.center;
            boxBounds.size = targ.bounds.size;

            EditorGUI.BeginChangeCheck();
            boxBounds.DrawHandle();
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(targ, "DBA Bounds");
                targ.bounds.center = boxBounds.center;
                targ.bounds.size = boxBounds.size;
                EditorUtility.SetDirty(target);
            }
        //}
    }
}
