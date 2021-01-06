using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreToUI : MonoBehaviour
{
    public TextMeshProUGUI textScoreTasks, textScoreUpgrades;
    public TextMeshProUGUI textUpgradeNeeded, textUpgradeCurrentAmount;
    public int points;
    TaskBlueprint selectedTask;
    

    public void ScoreToUIText()
    {
        points = TaskManager.GetPoints();
        textScoreTasks.text = TaskManager.GetPoints().ToString();
        textScoreUpgrades.text = TaskManager.GetPoints().ToString();

        textUpgradeCurrentAmount.text = ($"You have {TaskManager.GetPoints()} points!");


        textUpgradeNeeded.text = ($"You need {TaskManager.GetPoints()} more!");

    }
}
