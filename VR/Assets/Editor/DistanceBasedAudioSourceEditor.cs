using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CanEditMultipleObjects]
[CustomEditor(typeof(DistanceBasedAudioSource), true)]
public class DistanceBasedAudioSourceEditor : Editor {
    SerializedProperty bounds;
    BoxBoundsHandle boxBounds;

    void OnEnable() {
        bounds = serializedObject.FindProperty("bounds");
        boxBounds = new BoxBoundsHandle();
    }


    [DrawGizmo(GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(DistanceBasedAudioSource dba, GizmoType gizmoType) {
        if (dba.audioSource == null) return;

        GUIStyle customGUIStyle = new GUIStyle(GUI.skin.button);
        customGUIStyle.normal.textColor = Color.black;
        customGUIStyle.fontSize = 10;
        customGUIStyle.alignment = TextAnchor.MiddleCenter;
        GUI.backgroundColor = new Color(0.75f, 1.0f, 0.75f, 0.95f);
        Handles.Label(dba.transform.position, dba.name, customGUIStyle);

        Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
        Handles.CircleHandleCap(0, dba.transform.position, Quaternion.identity, dba.audioSource.maxDistance, EventType.Repaint);
        Handles.CircleHandleCap(0, dba.transform.position, Quaternion.identity, dba.audioSource.minDistance, EventType.Repaint);

        if (dba.useBounds) {
            if ((gizmoType & GizmoType.NotInSelectionHierarchy) != 0) {
                Gizmos.DrawWireCube(dba.transform.position + dba.bounds.center, dba.bounds.size);
                Gizmos.color = new Color(0.75f, 1.0f, 0.75f, 0.1f);
                Gizmos.DrawCube(dba.transform.position + dba.bounds.center, dba.bounds.size);
            }
        }
    }

    public void OnSceneGUI() {
        DistanceBasedAudioSource targ = (DistanceBasedAudioSource)target;
        if (targ.useBounds) {
            var localToWorld = Matrix4x4.TRS(targ.transform.position, targ.transform.rotation, Vector3.one);
            using (new Handles.DrawingScope(Color.white, localToWorld)) {
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
            }
        }
    }
}