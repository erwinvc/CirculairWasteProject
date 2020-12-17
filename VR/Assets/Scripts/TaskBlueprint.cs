using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBlueprint : MonoBehaviour {
    private string taskName;
    private string description;
    private int points;

    public TaskBlueprint(string name, string description, int points) {
        this.taskName = name;
        this.description = description;
        this.points = points;
    }

    private void Start() {
        TaskManager.RegisterTask(this);
#if UNITY_EDITOR
        if (transform.childCount != 1) Debug.LogError($"Task {taskName} doesn't contain a position object");
#endif
    }

    protected void FinishTask() {
        TaskManager.FinishTask(this);
        gameObject.SetActive(false);
    }

    public string GetName() {
        return taskName;
    }

    public string GetDescription() {
        return description;
    }

    public int GetPoints() {
        return points;
    }

    public abstract bool CanBeDone();

    public Vector3 GetPosition() {
        return transform.childCount > 0 ? transform.GetChild(0).position : transform.position;
    }
}
