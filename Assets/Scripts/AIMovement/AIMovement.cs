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
        public float StalkSpeed { get; set; }
        [SerializeField] private float _stalkSpeed;
        public NavMeshAgent _agent;
        private float _speed;
        public float _maxDistanceFromHome;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            StalkSpeed = _stalkSpeed;
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
