using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRabbit : ActionNode
{
    private FoxController _foxController;
    private AISensor _sensor;

    public AttackRabbit(FoxController foxController, FoxSensor sensor) : base(foxController, sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
    }

    public override BehaviorTreeStatus Update()
    {
        // Check if there are any rabbits within the sensor's range
        if (IsRabbitInRange())
        {
            // Find the closest rabbit within the sensor's range
            GameObject closestRabbit = SensorUtility.FindClosestTaggedGoInList(_sensor, _sensor.rabbitsInRange, "Rabbit");

            // Check if the fox is within attack range of the closest rabbit
            if (Vector3.Distance(_foxController.transform.position, closestRabbit.transform.position) <= _foxController.foxStats.AttackRange)
            {
                // Attack the rabbit
                Debug.Log("Attacking rabbit: " + closestRabbit.name);
                _foxController.Attack(closestRabbit);
                return BehaviorTreeStatus.SUCCESS;
            }
            else
            {
                Debug.Log("Moving towards rabbit: " + closestRabbit.name);
                // If the fox is not within attack range, move towards the rabbit
                _foxController.MoveTowards(closestRabbit.transform.position);
                return BehaviorTreeStatus.RUNNING;
            }
        }
        else
        {
            Debug.Log("No rabbits in range");
            // If there are no rabbits within the sensor's range, return a failure status
            return BehaviorTreeStatus.FAILURE;
        }
    }
    
    private bool IsRabbitInRange()
    {
        return _sensor.rabbitsInRange.Count > 0;
    }
}

