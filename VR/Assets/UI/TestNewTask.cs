using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNewTask : TaskBlueprint
{
    public TestNewTask() : base("SecondTask", "TestingTheSecondTask", 5)
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
