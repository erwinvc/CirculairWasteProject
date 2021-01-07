using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class OperateManualSorting : TaskBlueprint
{
    public TeleportPoint tpPoint1, tpPoint2, tpPoint3, tpPoint4;
    public OperateManualSorting() : base("Operate Manual Sorting", "Go and manually sort something", 50)
    {
    }

    private void FixedUpdate()
    {
        if (Teleport.instance.currentTeleportMarker == tpPoint1 ||  Teleport.instance.currentTeleportMarker == tpPoint2 ||
                    Teleport.instance.currentTeleportMarker == tpPoint3 ||  Teleport.instance.currentTeleportMarker == tpPoint4) {
            FinishTask();
        }
    }

    public override bool CanBeDone()
    {
        return true;
    }
}
