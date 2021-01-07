using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CompressorTask : TaskBlueprint
{
    public TeleportPoint tpPoint;
    public CompressorTask() : base("Watch compressor", "Find Teleport Point Near Compressor!", 100)
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
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