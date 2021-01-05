using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    private static TaskManager _Instance;

    private int points;
    public HashSet<TaskBlueprint> blueprints;
    private TaskBlueprint selectedTask;

    private TaskHighlighter highlighter;
    public Guide guide;

    void Awake() {
        DontDestroyOnLoad(this);
        _Instance = this;
        blueprints = new HashSet<TaskBlueprint>();
        highlighter = GetComponent<TaskHighlighter>();
        points = 0;
    }

    private void FixedUpdate() {
        highlighter.Highlight(selectedTask);
    }

    private void _FinishTask(TaskBlueprint task) {
        points += task.GetPoints();
        print($"{task.GetName()} completed!");
    }

    private void _RegisterTask(TaskBlueprint task) {
        if (blueprints.Contains(task)) throw new Exception($"Duplicated task registered: {task.GetName()}");
        blueprints.Add(task);
        selectedTask = task;
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

    public static HashSet<TaskBlueprint> GetTasks() {
        return _Instance.blueprints;
    }

    public static Guide GetGuide() {
        return _Instance.guide;
    }
}
