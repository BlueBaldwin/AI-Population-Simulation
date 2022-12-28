using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScent : ActionNode
{
    private FoxController _foxController;
    private FoxSensor _sensor;

    public FollowScent(FoxController foxController, FoxSensor sensor) : base(foxController, sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
    }

    public override BehaviorTreeStatus Update()
    {
        // Check if a scent is detected
        if (_sensor.IsScentDetected())
        {
            Debug.Log("Scent detected");
            // Follow the scent by moving towards the location of the rabbit droppings
            _foxController.MoveTowardsScent(_sensor.ScentLocation);

            // Check if the fox has reached the location of the rabbit droppings
            if (Vector3.Distance(_foxController.transform.position, _sensor.ScentLocation) < 0.1)
            {
                Debug.Log("Fox has reached scent location");
                return BehaviorTreeStatus.SUCCESS;
            }

            return BehaviorTreeStatus.RUNNING;
        }

        // If no scent is detected, return a failure status
        return BehaviorTreeStatus.FAILURE;
    }
}


