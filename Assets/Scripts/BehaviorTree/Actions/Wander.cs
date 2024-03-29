using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;

public class Wander : ActionNode
{
    private AIMovement _aiMovement;
    private NavMeshAgent _agent;
    private FoxController _foxController;
    private FoxSensor _sensor;

    public Wander(FoxController foxController, FoxSensor sensor, AIMovement aiMovement, NavMeshAgent agent) : base(foxController, sensor)
    {
        _aiMovement = aiMovement;
        _agent = agent;
        _foxController = foxController;
        _sensor = sensor;
    }
    
    public override BehaviorTreeStatus Update()
    {
        if (_sensor.rabbitsInRange.Count > 0)
        {
            return BehaviorTreeStatus.FAILURE;
        }
        // Check if the AI agent has a path or has reached its destination
        if (!_agent.hasPath && _agent.remainingDistance < 0.5)
        {
            // Generate a random point within the unit sphere centered at the origin
            Vector3 wanderPoint = Random.insideUnitSphere * _aiMovement._maxDistanceFromHome;

            // Use the NavMesh.SamplePosition function to find a valid point on the navmesh that is closest to the input point
            NavMeshHit hit;
            if (NavMesh.SamplePosition(wanderPoint, out hit, _aiMovement._maxDistanceFromHome, NavMesh.AllAreas))
            {
                // Set the destination for the navmesh agent to the random point on the navmesh
                _aiMovement.SetDestination(hit.position);
            }
        }

        return BehaviorTreeStatus.RUNNING;
    }
}

