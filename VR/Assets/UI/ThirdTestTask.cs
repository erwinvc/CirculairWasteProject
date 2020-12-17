using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTestTask : TaskBlueprint
{
    public ThirdTestTask() : base("Sorting matters", "Sort 5 pieces of trash", 100, false)
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
