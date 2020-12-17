using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBlueprint : MonoBehaviour {
    private string taskName;
    private string description;
    private int points;
    private bool selected;

    public TaskBlueprint(string name, string description, int points, bool selected) {
        this.taskName = name;
        this.description = description;
        this.points = points;
        this.selected = selected;
    }

    private void Start() {
        TaskManager.RegisterTask(this);
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

    public bool GetSelected()
    {
        return selected;
    }

    public abstract bool CanBeDone();
}
