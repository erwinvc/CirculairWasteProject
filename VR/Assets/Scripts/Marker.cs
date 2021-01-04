using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour {
    List<Node> path;
    private int pathIndex = 0;
    private Quaternion previousRotation;
    void Start() {

    }

    void FixedUpdate() {
        if (pathIndex >= 0) {
            transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].position + new Vector3(0.0f, 0.02f, 0.0f), Time.deltaTime * 2.5f);


            float distance = Vector3.Distance(transform.position, path[pathIndex].position);
            if (distance < 1.0f && pathIndex - 1 >= 0) {
                Quaternion lookOnLook = Quaternion.LookRotation(path[pathIndex - 1].position - transform.position);
                transform.rotation = Quaternion.Slerp(previousRotation, lookOnLook, 1.0f - distance);
            }

            if (distance < 0.05f) {
                if (pathIndex - 1 >= 0)
                    previousRotation = Quaternion.LookRotation(path[pathIndex - 1].position - transform.position);
                pathIndex--;
            }
        } else {
            Destroy(gameObject);
        }

        Node prev = null;
        for (int i = pathIndex; i >= 0; i--) {
            DebugLineRenderer.Draw(prev == null ? transform.position : prev.position, path[i].position, Color.green);
            prev = path[i];
        }
    }

    public void SetPath(List<Node> path) {
        this.path = path;
        pathIndex = path.Count - 1;

        previousRotation = Quaternion.LookRotation(path[pathIndex].position - transform.position);
        transform.rotation = previousRotation;
    }
}
