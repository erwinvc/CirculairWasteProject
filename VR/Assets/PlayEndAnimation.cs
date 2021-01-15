using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndAnimation : MonoBehaviour
{
    public static bool lookingAtTablet;
    public void Start()
    {
    }
    public void Update()
    {
        if (SceneHandler.upgrade > 3 && lookingAtTablet)
        {
        }
    }
}
