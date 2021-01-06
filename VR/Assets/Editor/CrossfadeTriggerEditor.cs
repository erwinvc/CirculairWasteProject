using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CrossFadeTrigger))]
[CanEditMultipleObjects]
public class CrossfadeTriggerEditor : Editor {

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(CrossFadeTrigger trigger, GizmoType gizmoType) {
        Gizmos.color = Color.grey;

        Gizmos.DrawWireCube(trigger.transform.position, trigger.transform.localScale);
        Gizmos.color = new Color(0.75f, 0.75f, 1.0f, 0.1f);
        Gizmos.DrawCube(trigger.transform.position, trigger.transform.localScale);
    }
}