using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    private FoxController _foxController;
    private FoxSensor _sensor;

    public ActionNode(FoxController foxController, FoxSensor sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
    }

    public abstract override BehaviorTreeStatus Update();
}
