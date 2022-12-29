
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;

public class StalkRabbit : ActionNode
{
    private FoxController _foxController;
    private FoxSensor _sensor;
    private AIMovement _aiMovement;
    private NavMeshAgent _agent;

    public StalkRabbit(FoxController foxController, FoxSensor sensor, AIMovement aiMovement) : base(foxController,
        sensor)
    {
        _foxController = foxController;
        _sensor = sensor;
        _aiMovement = aiMovement;
        _agent = aiMovement._agent;
    }

    public override BehaviorTreeStatus Update()
    {
        // Check if there are any rabbits within the sensor's range
        if (_sensor.rabbitsInRange.Count > 0)
        {
            // Find the closest rabbit within the sensor's range
            GameObject closestRabbit =
                SensorUtility.FindClosestTaggedGoInList(_sensor, _sensor.rabbitsInRange, "Rabbit");

            // Check if the fox is within attack range of the rabbit
            if (Vector3.Distance(_foxController.transform.position, closestRabbit.transform.position) <=
                _foxController.foxStats.AttackRange)
            {
                // If the fox is within attack range, return SUCCESS
                return BehaviorTreeStatus.SUCCESS;
            }
            else
            {
                // If the fox is not within attack range, slow down the movement speed and move towards the rabbit
                _agent.speed = _aiMovement.StalkSpeed;
                return BehaviorTreeStatus.RUNNING;
            }
        }

        return BehaviorTreeStatus.FAILURE;
    }
}

