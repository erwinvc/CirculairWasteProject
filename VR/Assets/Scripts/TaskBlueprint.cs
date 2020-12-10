using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBlueprint : MonoBehaviour {
    private string name;
    private string description;
    private int points;

    public TaskBlueprint(string name, string description, int points) {
        this.name = name;
        this.description = description;
        this.points = points;
    }

    private void Start() {
        TaskManager.RegisterTask(this);
    }

    protected void FinishTask() {
        TaskManager.FinishTask(this);
        gameObject.SetActive(false);
    }

    public string GetName() {
        return name;
    }

    public string GetDescription() {
        return description;
    }

    public int GetPoints() {
        return points;
    }
}
