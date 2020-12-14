using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Guide))]
public class GuideEditor : Editor {
    private static Node selectedNode;

    public void OnEnable() {
        Guide guide = target as Guide;
        selectedNode = guide.baseNode;
    }

    public void OnSceneGUI() {
        Guide guide = target as Guide;
        OnSceneGUINode(guide.baseNode, null);
    }

    void OnSceneGUINode(Node node, Node parent) {
        Guide guide = target as Guide;

        if (GetKeyDown(KeyCode.Delete)) {
            DeleteNode();
            ConsumeEvent();
        }

        if (parent != null) {
            Handles.color = node == selectedNode || parent == selectedNode ? Color.cyan : Color.white;
            Handles.DrawLine(node.position, parent.position);
        }
        if (node == selectedNode) {
            Undo.RecordObject(guide, "MovingNode");
            node.position = Handles.PositionHandle(node.position, Quaternion.identity);
        } else {
            Handles.color = node == guide.baseNode ? Color.red : Color.white;
            if (Handles.Button(node.position, Quaternion.identity, 0.2f, 0.2f, Handles.SphereHandleCap)) {
                if (Event.current.type == EventType.KeyDown)
                    Debug.Log("Shift");
                selectedNode = node;
            }

        }

        if (node.children != null) {
            foreach (Node n in node.children) {
                OnSceneGUINode(n, node);
            }
        }
    }

    public override void OnInspectorGUI() {
        Guide guide = target as Guide;

        serializedObject.Update();

        DrawDefaultInspector();

        if (selectedNode != null) {
            Undo.RecordObject(guide, "MovingNode");
            selectedNode.position = EditorGUILayout.Vector3Field("Selected node position", selectedNode == null ? new Vector3(0, 0, 0) : selectedNode.position);

            if (GUILayout.Button("Add child node")) {
                AddNode();
            }
            EditorGUI.BeginDisabledGroup(selectedNode == guide.baseNode);
            if (GUILayout.Button("Delete node")) {
                DeleteNode();
            }
            EditorGUI.EndDisabledGroup();
        } else {
            EditorGUI.BeginDisabledGroup(true);
            GUILayout.Button("Add child node");
            GUILayout.Button("Delete node");
            EditorGUILayout.Vector3Field("Selected node position", new Vector3(0, 0, 0));
            EditorGUI.EndDisabledGroup();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DeleteNode() {
        Guide guide = target as Guide;
        Undo.RecordObject(guide, "DeletingNode");

        Node tempNode = selectedNode;
        selectedNode = selectedNode.parent;
        tempNode.Delete();
    }

    void AddNode() {
        Guide guide = target as Guide;
        Undo.RecordObject(guide, "AddingNode");
        Vector3 position = SceneView.lastActiveSceneView.pivot;
        position.y = selectedNode.position.y;
        Node newNode = new Node(position);

        selectedNode.AddChild(newNode);
        selectedNode = newNode;
    }

    bool GetKeyDown(KeyCode keyCode, EventModifiers modifier) {
        return (Event.current.type == EventType.KeyDown && Event.current.keyCode == keyCode && Event.current.modifiers == modifier);
    }

    bool GetKeyDown(KeyCode keyCode) {
        return (Event.current.type == EventType.KeyDown && Event.current.keyCode == keyCode);
    }

    void ConsumeEvent() {
        GUIUtility.hotControl = 0;
        Event.current.Use();
    }
}

