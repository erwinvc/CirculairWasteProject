using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    private static TaskManager _Instance;

    private int points;
    public MonoBehaviour testt;
    public HashSet<TaskBlueprint> blueprints;

    void Start() {
        _Instance = this;
        blueprints = new HashSet<TaskBlueprint>();
        points = 0;
    }

    private void _FinishTask(TaskBlueprint task) {
        points += task.GetPoints();
        print($"{task.GetName()} completed!");
    }

    private void _RegisterTask(TaskBlueprint task) {
        if (blueprints.Contains(task)) throw new Exception($"Duplicated task registered: {task.GetName()}");
        blueprints.Add(task);
    }

    public static void FinishTask(TaskBlueprint task) {
        _Instance._FinishTask(task);
    }

    public static void RegisterTask(TaskBlueprint task) {
        _Instance._RegisterTask(task);
    }

    public static int GetPoints() {
        return _Instance.points;
    }
}
