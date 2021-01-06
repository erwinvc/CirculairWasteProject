using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpatialAudioSettings))]
[CanEditMultipleObjects]
public class SpatialAudioSettingsEditor : Editor {

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(SpatialAudioSettings settings, GizmoType gizmoType) {
        Gizmos.color = Color.grey;

        Gizmos.DrawWireCube(settings.transform.position, settings.transform.localScale);
        Gizmos.color = new Color(1.0f, 0.75f, 0.75f, 0.1f);
        Gizmos.DrawCube(settings.transform.position, settings.transform.localScale);

        GUIStyle customGUIStyle = new GUIStyle(GUI.skin.button);
        customGUIStyle.normal.textColor = Color.black;
        customGUIStyle.fontSize = 12;
        customGUIStyle.alignment = TextAnchor.MiddleCenter;
        GUI.backgroundColor = new Color(1.0f, 0.75f, 0.75f, 0.95f);
        Handles.Label(settings.transform.position, settings.name + "\n Preset: " + settings.preset.ToString(), customGUIStyle);
    }
}
