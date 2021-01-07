using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    private static TaskManager _Instance;

    private int points;
    public List<TaskBlueprint> blueprints;
    private TaskBlueprint selectedTask;
    public FillTaskList ftl;

    public Guide guide;
    private int completedTasks = 0;
    public GameObject highligher;

    void Awake() {
        DontDestroyOnLoad(this);
        _Instance = this;
        blueprints = new List<TaskBlueprint>();
        points = 0;
    }

    private void FixedUpdate() {
        if (blueprints.Count > 0 && selectedTask != null) {
            highligher.SetActive(true);
            highligher.GetComponent<LookAtPlayer>().position = blueprints[0].GetPosition();
        } else highligher.SetActive(false);
    }

    private void _FinishTask(TaskBlueprint task) {
        points += task.GetPoints();
        print($"{task.GetName()} completed!");
        blueprints.Remove(task);
        ftl.UpdateTaskList();
        if (completedTasks == 0) {
            SoundEffectManager.Play("FirstTaskCompleted");
        } else if (completedTasks == 1) {
            SoundEffectManager.Play("SecondTaskCompleted");
        } else {
            SoundEffectManager.Play("TaskCompleted");
        }
        completedTasks++;
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

    public static List<TaskBlueprint> GetTasks() {
        return _Instance.blueprints;
    }

    public static Guide GetGuide() {
        return _Instance.guide;
    }
}
