using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTestTask : TaskBlueprint
{
    public ThirdTestTask() : base("ThirdTask", "This is the description of the third task", 5)
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
