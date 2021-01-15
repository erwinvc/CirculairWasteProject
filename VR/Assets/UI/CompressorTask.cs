using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CompressorTask : TaskBlueprint
{
    public GameObject compressor;
    public CompressorTask() : base("Watch compressor", "Move to the compressor!", 100)
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (compressor != null)
        {
            if (Vector3.Distance(GlobalPlayer.globalObject.transform.position, compressor.transform.position) < 3f)
            {
                FinishTask();
            }
        } else
        {
            return;
        }
    }

    public override bool CanBeDone()
    {
        return true;
    }
}