using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreToUI : MonoBehaviour
{
    public TextMeshProUGUI textScoreTasks, textScoreUpgrades;
    

    public void ScoreToUIText()
    {
        textScoreTasks.text = TaskManager.GetPoints().ToString();
        textScoreUpgrades.text = TaskManager.GetPoints().ToString();
    }
}
