using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {
    public Node baseNode = new Node(new Vector3(0, 0, 0));

    [NonSerialized]
    public List<Node> bakedNodes;
    private void Start() {
        Bake();
    }

    void Bake() {
        FixParenting();
        bakedNodes = new List<Node>();
        baseNode.CollectChildNodes(bakedNodes);
    }

    void FixParenting() {
        List<Node> nodes = new List<Node>();
        baseNode.CollectChildNodes(nodes);
        foreach (Node node in nodes) {
            foreach (Node other in nodes) {
                if (node.children != null) {
                    if (node.children.Contains(other)) {
                        other.parent = node;
                    }
                }
            }
        }
    }

    public Node FindClosestNode(Vector3 position) {
        Node closest = null;
        float previousDistance = float.MaxValue;
        foreach (Node node in bakedNodes) {
            float distance = Vector3.Distance(position, node.position);
            if (distance < previousDistance) {
                closest = node;
                previousDistance = distance;
            }
        }

        return closest;
    }

    public List<Node> FindPath(Node start, Node end) {
        List<Node> nodes = new List<Node>();
        if (!start.FindPath(end, nodes, null)) {
            print("failed f");
        };
        return nodes;
    }
}
