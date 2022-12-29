using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScent : ActionNode
{
    private FoxController _foxController;
    private FoxSensor _sensor;
    private HashSet<Vector3> _visitedScentLocations;

    public FollowScent(FoxController foxController, FoxSensor sensor) : base(foxController, sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
        _visitedScentLocations = new HashSet<Vector3>();
    }

    public override BehaviorTreeStatus Update()
    {
        // Check if a scent is detected
        if (_sensor.IsScentDetected())
        {
            if (_visitedScentLocations.Contains(_sensor.ScentLocation))
            {
                // If the fox has already visited this scent location, return a failure status
                return BehaviorTreeStatus.FAILURE;
            }

            Debug.Log("Scent detected");
            // Follow the scent by moving towards the location of the rabbit droppings
            _foxController.MoveTowardsScent(_sensor.ScentLocation);

            // Check if the fox has reached the location of the rabbit droppings
            if (_foxController._aiMovement._agent.hasPath && _foxController._aiMovement._agent.remainingDistance < 0.1)
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


