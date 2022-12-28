using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTired : ConditionNode
{
    private FoxController _foxController;

    public IsTired(FoxController foxController)
    {
        _foxController = foxController;
    }

    public override bool Check()
    {
        return _foxController.IsTired();
    }

    public override BehaviorTreeStatus Update()
    {
        if (_foxController.IsTired())
        {
            return BehaviorTreeStatus.SUCCESS;
        }
        else
        {
            return BehaviorTreeStatus.FAILURE;
        }
    }
}
