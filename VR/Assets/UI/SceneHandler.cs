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

    public FillTaskList ftl;
    public Scrollbar sb;

    private bool pointerOnScroll;
    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
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
        }

        if(e.target.name == sb.name)
        {
            Debug.Log("HIT");
            sb.value = 0;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == sb.name)
        {
            pointerOnScroll = true;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == sb.name)
        {
            pointerOnScroll = false;
        }
    }
}