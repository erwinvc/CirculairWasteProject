using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CrusherTask : TaskBlueprint
{
    public static bool Finished = false;
    public CrusherTask() : base("Manually Crush", "Manually crush a bottle!", 100)
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (Finished)
        {
            FinishTask();
        }
    }

    public override bool CanBeDone()
    {
        return true;
    }
}