using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNewTask : TaskBlueprint
{
    public TestNewTask() : base("Mess around", "Touch the floor", 10, false)
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
