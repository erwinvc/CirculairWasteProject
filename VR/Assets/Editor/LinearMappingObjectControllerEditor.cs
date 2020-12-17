

using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CanEditMultipleObjects]
[CustomEditor(typeof(LinearMappingObjectController), true)]
public class LinearMappingObjectControllerEditor : Editor {
    BoxBoundsHandle boxBounds;

    void OnEnable() {
        boxBounds = new BoxBoundsHandle();
    }

    public void OnSceneGUI() {
        LinearMappingObjectController lmoc = (LinearMappingObjectController)target;
        boxBounds.center = lmoc.bounds.center;
        boxBounds.size = lmoc.bounds.size;

        EditorGUI.BeginChangeCheck();
        boxBounds.DrawHandle();
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(lmoc, "LMOC Bounds");
            lmoc.bounds.center = boxBounds.center;
            lmoc.bounds.size = boxBounds.size;
            EditorUtility.SetDirty(target);
        }
    }
}


