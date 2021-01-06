using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(SoundtrackManager))]
[CanEditMultipleObjects]
public class SoundtrackManagerEditor : Editor {
    private ReorderableList list;
    private SerializedProperty soundTrackDefinitions;
    private int globalIndex;

    void OnEnable() {
        soundTrackDefinitions = serializedObject.FindProperty("soundTrackDefinitions");
        list = new ReorderableList(serializedObject, soundTrackDefinitions, true, true, true, true);

        list.onAddCallback += AddItem;
        list.drawElementCallback += DrawElement;
        list.drawHeaderCallback += DrawHeader;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        globalIndex = list.index;

        if (globalIndex != -1 && globalIndex < soundTrackDefinitions.arraySize) {
            EditorGUILayout.LabelField("Sound Track Properties", EditorStyles.boldLabel);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            serializedObject.Update();
            SerializedProperty soundEffectDefinition = soundTrackDefinitions.GetArrayElementAtIndex(globalIndex);
            SerializedProperty name = soundEffectDefinition.FindPropertyRelative("name");
            SerializedProperty clip = soundEffectDefinition.FindPropertyRelative("clip");
            SerializedProperty volume = soundEffectDefinition.FindPropertyRelative("volume");

            EditorGUILayout.PropertyField(name, new GUIContent("Name"));
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(clip);
            volume.floatValue = EditorGUILayout.Slider("Volume", volume.floatValue, 0.0f, 1.0f);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
    }

    private void OnDisable() {
        list.onAddCallback -= AddItem;
        list.drawElementCallback -= DrawElement;
        list.drawHeaderCallback -= DrawHeader;
    }

    private void AddItem(ReorderableList list) {
        SoundtrackManager mgr = target as SoundtrackManager;
        mgr.soundTrackDefinitions.Add(new SoundtrackManager.SoundtrackDefinition());

        EditorUtility.SetDirty(target);
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused) {
        SoundtrackManager targ = target as SoundtrackManager;
        string name = targ.soundTrackDefinitions[index].name;
        if (name.Length == 0) name = "{Empty}";
        GUI.Label(rect, name);
    }

    private void DrawHeader(Rect rect) {
        GUI.Label(rect, "Soundtracks");
    }
}