using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTaskList : MonoBehaviour
{
    public TaskManager tm;
    private TaskBlueprint task;
    public GameObject taskPrefab;
    public GameObject content;
    private GameObject taskTest;
    private int offSet;

    private void Start()
    {
        offSet = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed");
            UpdateTaskList();
        }
    }

    public void UpdateTaskList()
    {
        foreach (TaskBlueprint task in tm.blueprints)
        {
            taskTest = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity);
            taskTest.transform.SetParent(content.transform);
            taskTest.transform.localScale = taskPrefab.transform.localScale;
            taskTest.transform.position = new Vector3(0, 0 + offSet, 0);
            offSet += 20;

            Debug.Log(task.GetName());
            Debug.Log(taskTest.transform.ToString());
        }
    }
}
