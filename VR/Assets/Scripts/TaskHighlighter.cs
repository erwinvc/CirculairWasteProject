using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TaskHighlighter : MonoBehaviour {
    public GameObject marker;
    private GameObject player;
    private float timer = 0;

    void Start() {
        player = GameObject.Find("Player");
    }

    void Update() {

    }

    public void Highlight(TaskBlueprint selectedTask) {
        if (selectedTask == null) return;
        timer += Time.deltaTime;
        if (timer > 1) {
            timer -= 1;
            SpawnMarker(selectedTask);
        }
    }

    private void SpawnMarker(TaskBlueprint selectedTask)
    {
        Node playerNode = TaskManager.GetGuide().FindClosestNode(player.transform.position);
        Node goalNode = TaskManager.GetGuide().FindClosestNode(selectedTask.GetPosition());

        Marker markerObj = Instantiate(marker, player.transform.position, Quaternion.identity).GetComponent<Marker>();
        markerObj.SetPath(TaskManager.GetGuide().FindPath(playerNode, goalNode));
    }
}
