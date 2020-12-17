using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTestTask : TaskBlueprint
{
    public ThirdTestTask() : base("Dummy Task", "This is a dummy task", 100)
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
