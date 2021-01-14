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
       // Debug.Log(Vector3.Distance(player.transform.position, compressor.transform.position));
        if (Vector3.Distance(GlobalPlayer.globalObject.transform.position, compressor.transform.position) < 10f)
        {
            FinishTask();
        }
    }

    public override bool CanBeDone()
    {
        return true;
    }
}