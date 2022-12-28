using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHungry : ConditionNode
{
    private FoxController _foxController;
    
    public IsHungry(FoxController foxController)
    {
        _foxController = foxController;
    }

    public override bool Check()
    {
        return _foxController.IsHungry();
    }
}
