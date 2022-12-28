using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForPrey : ActionNode
{
    private FoxController _foxController;
    private FoxSensor _sensor;

    public SearchForPrey(FoxController foxController, FoxSensor sensor) : base(foxController, sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
    }

    public override BehaviorTreeStatus Update()
    {
        // Check if the fox is tired
        if (_foxController.IsTired())
        {
            return BehaviorTreeStatus.FAILURE;
        }

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

        if (_sensor.GetRabbitsInRange().Count > 0)
        {
            Debug.Log("Rabbit Detected");
            return BehaviorTreeStatus.RUNNING;
        }
        else
        {
            // If no scent is detected, wander randomly
            _foxController.Wander();
            Debug.Log("Fox is wandering");
            return BehaviorTreeStatus.RUNNING;
        }
    }
}


