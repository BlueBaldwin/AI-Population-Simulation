using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UtilityAi
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private float _speed;
        //public Transform destination;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        
        private void SetAgentSpeed(float speed)
        {
            _agent.speed = speed;
        }
        
        public Vector3 GetDestination()
        {
            return _agent.destination;
        }

        public void SetDestination(Vector3 position)
        {
            _agent.destination = position;
        }
        
    }
}
