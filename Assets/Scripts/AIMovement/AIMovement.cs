using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    //public Transform destination;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 position) {_agent.destination = position;}
    
}
