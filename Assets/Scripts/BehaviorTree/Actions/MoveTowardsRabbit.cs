using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;

public class MoveTowardsRabbit : ActionNode
{
    private FoxController _foxController;
    private FoxSensor _sensor;
    private AIMovement _aiMovement;
    private NavMeshAgent _agent;

    public MoveTowardsRabbit(FoxController foxController, FoxSensor sensor, AIMovement aiMovement) : base(foxController, sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
        _agent = aiMovement._agent;
    }

    public override BehaviorTreeStatus Update()
    {
        // Check if there are any rabbits within the sensor's range
        if (_sensor.rabbitsInRange.Count > 0)
        {
            // Find the closest rabbit within the sensor's range
            GameObject closestRabbit = SensorUtility.FindClosestTaggedGoInList(_sensor, _sensor.rabbitsInRange, "Rabbit");

            _agent.SetDestination(closestRabbit.transform.position);

            // Check if the fox has reached its destination
            if (_agent.remainingDistance < 0.1f)
            {
                // Return SUCCESS if the fox has reached its destination
                return BehaviorTreeStatus.SUCCESS;
            }
            else
            {
                // Return RUNNING if the fox is still moving towards its destination
                return BehaviorTreeStatus.RUNNING;
            }
        }
        else
        {
            // If there are no rabbits within the sensor's range, return a failure status
            return BehaviorTreeStatus.FAILURE;
        }
    }
}

