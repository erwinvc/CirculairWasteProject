using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreToUI : MonoBehaviour
{
    public TextMeshProUGUI textScoreTasks, textScoreUpgrades;
    public TextMeshProUGUI textUpgradeNeeded, textUpgradeCurrentAmount;
    public FillTaskList ftl;
    public int currentPoints;

    public int pointsUntilUpgrade = 100;
    int pointsTemp;

    public void ScoreToUIText()
    {
        currentPoints = TaskManager.GetPoints();
        textScoreTasks.text = TaskManager.GetPoints().ToString();
        textScoreUpgrades.text = TaskManager.GetPoints().ToString();
    }

    public void CalculateRemaining()
    {
        currentPoints = TaskManager.GetPoints();
        pointsTemp = pointsUntilUpgrade;
        pointsUntilUpgrade = pointsUntilUpgrade - currentPoints;
        textUpgradeCurrentAmount.text = ($"You have {TaskManager.GetPoints()} points!");
        textUpgradeNeeded.text = ($"You need {pointsUntilUpgrade.ToString()} more!");
        pointsUntilUpgrade = pointsTemp;
    }
}
