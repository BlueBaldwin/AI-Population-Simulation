using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionNode : Node
{
    public abstract bool Check();

    public override BehaviorTreeStatus Update()
    {
        if (Check())
        {
            return BehaviorTreeStatus.SUCCESS;
        }
        else
        {
            return BehaviorTreeStatus.FAILURE;
        }
    }
}

