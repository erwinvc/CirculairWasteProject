using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class OperateClawTask : TaskBlueprint {
    public TeleportPoint tpPoint;
    public OperateClawTask() : base("Operator", "Operate the claw", 100, true) {
    }

    private void FixedUpdate() {
        if (Teleport.instance.currentTeleportMarker == tpPoint) {
            FinishTask();
        }
    }

    public override bool CanBeDone() {
        return true;
    }
}