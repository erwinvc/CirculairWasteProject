using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SoundEffectManager))]
[CanEditMultipleObjects]
public class SoundEffectManagerEditor : Editor {
    private ReorderableList list;
    private ReorderableList listsClips;
    private SerializedProperty soundEffectDefinitions;
    private int globalIndex;
    private bool muteToggle = false;

    private void OnEnable() {
        soundEffectDefinitions = serializedObject.FindProperty("soundEffectDefinitions");
        list = new ReorderableList(serializedObject, soundEffectDefinitions, true, true, true, true);
        listsClips = new ReorderableList(serializedObject, null, true, true, true, true);

        list.onAddCallback += AddItem;
        list.drawElementCallback += DrawElement;
        list.drawHeaderCallback += DrawHeader;
        listsClips.drawElementCallback += DrawElementClip;
        listsClips.drawHeaderCallback += DrawHeaderClip;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        globalIndex = list.index;

        if (globalIndex != -1 && globalIndex < soundEffectDefinitions.arraySize) {
            EditorGUILayout.LabelField("Sound Effect Properties", EditorStyles.boldLabel);
            GUILayout.BeginVertical(EditorStyles.helpBox);

            serializedObject.Update();
            SerializedProperty soundEffectDefinition = soundEffectDefinitions.GetArrayElementAtIndex(globalIndex);
            SerializedProperty name = soundEffectDefinition.FindPropertyRelative("name");
            SerializedProperty clips = soundEffectDefinition.FindPropertyRelative("clips");
            SerializedProperty volume = soundEffectDefinition.FindPropertyRelative("volume");
            SerializedProperty randomPitch = soundEffectDefinition.FindPropertyRelative("randomPitch");
            SerializedProperty pitchMax = soundEffectDefinition.FindPropertyRelative("pitchMax");
            SerializedProperty pitchMin = soundEffectDefinition.FindPropertyRelative("pitchMin");

            EditorGUILayout.PropertyField(name, new GUIContent("Name"));
            EditorGUILayout.Space();

            listsClips.serializedProperty = clips;
            listsClips.DoLayoutList();
            EditorGUILayout.Space();
            volume.floatValue = EditorGUILayout.Slider("Volume", volume.floatValue, 0.0f, 1.0f);
            randomPitch.boolValue = EditorGUILayout.Toggle("Random pitch", randomPitch.boolValue);
            if (randomPitch.boolValue) {
                pitchMax.floatValue =
                    EditorGUILayout.Slider("Pitch max", pitchMax.floatValue, pitchMin.floatValue, 10.0f);
                pitchMin.floatValue =
                    EditorGUILayout.Slider("Pitch min", pitchMin.floatValue, -10.0f, pitchMax.floatValue);
            } else {
                pitchMax.floatValue = EditorGUILayout.Slider("Pitch", pitchMax.floatValue, -10.0f, 10.0f);
            }

            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
    }

    private void OnDisable() {
        list.onAddCallback -= AddItem;
        list.drawElementCallback -= DrawElement;
        list.drawHeaderCallback -= DrawHeader;
        listsClips.drawElementCallback -= DrawElementClip;
        listsClips.drawHeaderCallback -= DrawHeaderClip;
    }

    private void AddItem(ReorderableList list) {
        SoundEffectManager mgr = target as SoundEffectManager;
        mgr.soundEffectDefinitions.Add(new SoundEffectManager.SoundEffectDefinition());

        EditorUtility.SetDirty(target);
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused) {
        SoundEffectManager targ = target as SoundEffectManager;
        string name = targ.soundEffectDefinitions[index].name;
        if (name.Length == 0) name = "{Empty}";
        GUI.Label(rect, name);
        Rect rectNew = new Rect(rect.x + rect.width - 20, rect.y, 20, rect.height);

        SerializedProperty soundEffectDefinition = soundEffectDefinitions.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(rectNew, soundEffectDefinition.FindPropertyRelative("muted"), new GUIContent());
    }

    private void DrawElementClip(Rect rect, int index, bool active, bool focused) {
        SerializedProperty soundEffectDefinition = soundEffectDefinitions.GetArrayElementAtIndex(globalIndex);
        SerializedProperty clip = soundEffectDefinition.FindPropertyRelative("clips").GetArrayElementAtIndex(index);
        EditorGUI.PropertyField(rect, clip);
    }

    private void DrawHeader(Rect rect) {
        GUI.Label(rect, "Sound Effects");
        Rect toggleRect = new Rect(rect.x + rect.width - 20, rect.y, 20, rect.height);
        Rect labelRect = new Rect(rect.x + rect.width - 65, rect.y, 40, rect.height);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleRight;
        GUI.Label(labelRect, "Muted", style);

        bool oldMuteToggle = muteToggle;
        muteToggle = EditorGUI.Toggle(toggleRect, muteToggle);
        if (muteToggle != oldMuteToggle) {
            serializedObject.Update();
            for (int i = 0; i < soundEffectDefinitions.arraySize; i++) {
                soundEffectDefinitions.GetArrayElementAtIndex(i).FindPropertyRelative("muted").boolValue = muteToggle;
            }
            serializedObject.ApplyModifiedProperties();
        }

    }

    private void DrawHeaderClip(Rect rect) {
        GUI.Label(rect, "Audio Clips");
    }
}