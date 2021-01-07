using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MagnetTask : TaskBlueprint
{
    public static bool Finished = false;
    public MagnetTask() : base("Activate Magnet", "Press the button on the railway!", 100)
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if(Finished)
        {
            FinishTask();
        }
    }

    public override bool CanBeDone()
    {
        return true;
    }
}