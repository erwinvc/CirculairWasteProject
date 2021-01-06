using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuallySortTask : TaskBlueprint
{
    public ManuallySortTask() : base("Manually Sort", "Touch the floor", 50)
    {
    }

    private void FixedUpdate()
    {
        
    }

    public override bool CanBeDone()
    {
        return true;
    }
}
