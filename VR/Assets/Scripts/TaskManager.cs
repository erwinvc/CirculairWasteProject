using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

private TaskManager _Instance;
    public Type type;
    public MonoBehaviour testt;
    public List<TaskBlueprint> blueprints;

    void Start() {
        _Instance = this;
    }

    void Update() {

    }
}
