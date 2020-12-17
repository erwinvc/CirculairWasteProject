using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node {
    private int depth;
    public Node parent;
    public Vector3 position;
    public List<Node> children = new List<Node>();

    public Node(Vector3 position) {
        this.position = position;
        depth = 0;
    }

    public void AddChild(Node node) {
        children.Add(node);
        node.SetParent(depth + 1, this);
    }

    private void SetParent(int depth, Node node) {
        this.depth = depth;
        parent = node;
        if (depth >= 7) throw new Exception("Too many nested nodes");
    }

    public void Delete() {
        if (parent != null) {
            parent.children.Remove(this);
        }
    }

    public void CollectChildNodes(List<Node> nodes) {
        nodes.Add(this);
        if (children != null) {
            foreach (var child in children) {
                child.CollectChildNodes(nodes);
            }
        }
    }

    public bool FindPath(Node end, List<Node> nodes, Node sourceNode) {
        if (this == end) {
            nodes.Add(this);
            return true;
        }

        if (children != null) {
            foreach (var child in children) {
                if (child == sourceNode) continue;
                if (child.FindPath(end, nodes, this)) {
                    nodes.Add(this);
                    return true;
                }
            }
        }

        if (parent != null && parent != sourceNode && parent.FindPath(end, nodes, this)) {
            nodes.Add(this);
            return true;
        }
        return false;
    }
}
