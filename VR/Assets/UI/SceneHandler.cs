/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public GameObject welcomeSceenDisplay;
    public GameObject tasksScreenDisplay;
    public GameObject upgradesScreenDisplay;

    public ScoreToUI scoreToUI;

    public FillTaskList ftl;
    public Scrollbar sb;
    public GameObject content;

    private bool pointerOnScroll;
    void Awake()
    {
        //laserPointer.PointerIn += PointerInside;
        //laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }
    
    public void PointerClick(object  sender, PointerEventArgs e)
    {
        Debug.Log(e.target.name);
        if (e.target.name == "ButtonBegin")
        {
            welcomeSceenDisplay.SetActive(false);
            tasksScreenDisplay.SetActive(true);
            ftl.UpdateTaskList();
            scoreToUI.ScoreToUIText();
        }

        if (e.target.name == "BtnNavUpgrades")
        {
            tasksScreenDisplay.SetActive(true);
            upgradesScreenDisplay.SetActive(true);
            scoreToUI.ScoreToUIText();
        }

        if (e.target.name == "BtnNavTasks")
        {
            upgradesScreenDisplay.SetActive(false);
            tasksScreenDisplay.SetActive(true);
            scoreToUI.ScoreToUIText();
        }

        if(e.target.name == "BtnUpgrade")
        {

        }
        if (e.target.CompareTag("UIClickable"))
        {
            foreach (Transform child in content.transform)
            {
                child.GetComponent<Image>().color = Color.black;
            }
            e.target.gameObject.GetComponent<Image>().color = Color.green;
        }

        if (e.target.name == "MoveScrollUp")
        {
            sb.value += 0.1f;
        }

        if (e.target.name == "MoveScrollDown")
        {
            sb.value -= 0.1f;
        }
    }
}