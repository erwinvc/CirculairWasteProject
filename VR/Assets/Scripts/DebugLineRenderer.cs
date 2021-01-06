using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class DebugLineRenderer : MonoBehaviour {
    private struct Line {
        public Vector3 start;
        public Vector3 end;
        public Color color;

        public Line(Vector3 s, Vector3 e, Color c) {
            start = s;
            end = e;
            color = c;
        }
    }

    private Mesh mesh;
    private MeshFilter filter;
    private List<Line> lines;
    private static DebugLineRenderer _Instance;
    private static Color defaultColor = new Color(1, 0, 0, 1);

    void Start() {
        DontDestroyOnLoad(this);
        _Instance = this;
        lines = new List<Line>();
        mesh = new Mesh();
        filter = GetComponent<MeshFilter>();
    }

    private void Update() {
        CreateMesh();
    }

    void CreateMesh() {
        mesh.Clear();

        int verticesCount = lines.Count * 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] indices = new int[verticesCount];
        Color[] colors = new Color[verticesCount];
        for (int i = 0; i < lines.Count; i++) {
            vertices[i * 2] = lines[i].start;
            vertices[i * 2 + 1] = lines[i].end;
            colors[i * 2] = lines[i].color;
            colors[i * 2 + 1] = lines[i].color;

            indices[i * 2] = i * 2;
            indices[i * 2 + 1] = i * 2 + 1;
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.SetIndices(indices, MeshTopology.Lines, 0, true);

        filter.sharedMesh = mesh;

        lines.Clear();
    }

    public static void Draw(Vector3 start, Vector3 end, Color color) {
        _Instance.lines.Add(new Line(start, end, color));
    }
}

#else

public class DebugLineRenderer : MonoBehaviour {
    void Start() {
        DontDestroyOnLoad(this);
    }

    public static void Draw(Vector3 start, Vector3 end, Color color) {
    }
}

#endif