using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ManuallySortTask : TaskBlueprint
{
    public TeleportPoint tpPoint;
    public ManuallySortTask() : base("Manually Sort", "Touch the floor", 50)
    {
    }

    private void FixedUpdate()
    {
        if (Teleport.instance.currentTeleportMarker == tpPoint)
        {
            FinishTask();
        }
    }

    public override bool CanBeDone()
    {
        return true;
    }
}
