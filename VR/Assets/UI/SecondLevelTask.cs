using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelTask : TaskBlueprint
{
    public SecondLevelTask() : base("Dummy Task", "This is a dummy task", 100)
    {
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void FixedUpdate()
    {

    }

    public override bool CanBeDone()
    {
        return true;
    }
}
