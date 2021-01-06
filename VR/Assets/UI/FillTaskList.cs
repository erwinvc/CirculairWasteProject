﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FillTaskList : MonoBehaviour
{
    public TaskManager tm;
    public GameObject taskPrefab;
    public GameObject content;
    private GameObject taskTest;
    private Vector3 offSet;

    private bool firstTime;
    private TextMeshProUGUI textComponent;
    public ScoreToUI stUI;

    private void Start()
    {
        offSet = new Vector3(0, 0, 0);
    }

    public void UpdateTaskList()
    {
        stUI.CalculateRemaining();

        if (firstTime)
        {
            foreach (Transform child in content.transform)
            {
                offSet = Vector3.zero;
                Destroy(child.gameObject);
            }
        }

        firstTime = true;

        foreach (TaskBlueprint task in tm.blueprints)
        {
            Debug.Log(task.GetName());
            taskTest = Instantiate(taskPrefab, taskPrefab.transform.localPosition + offSet, Quaternion.identity);
            taskTest.transform.SetParent(content.transform, false);
            taskTest.transform.localScale = taskPrefab.transform.localScale;
            offSet.y -= 15;


            //check all of the children of the prefab 
            foreach (Transform child in taskTest.transform)
            {
                string childName = child.name;

                //fill field text based on result
                switch (childName)
                {
                    case "TaskTitle":
                        textComponent = child.GetComponent<TextMeshProUGUI>();
                        textComponent.text = task.GetName();
                        break;
                    case "TaskDescription":
                        textComponent = child.GetComponent<TextMeshProUGUI>();
                        textComponent.text = task.GetDescription();
                        break;
                    case "TaskPoints":
                        textComponent = child.GetComponent<TextMeshProUGUI>();
                        textComponent.text = task.GetPoints().ToString();
                        break;
                    default:
                        Debug.Log("no match");
                        break;
                }
            }
        }
    }
}
