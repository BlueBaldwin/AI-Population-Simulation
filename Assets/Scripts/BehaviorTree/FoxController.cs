using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;

namespace BehaviorTree
{
    public class FoxController : MonoBehaviour
    {
        private FoxBehaviorTree _behaviorTree;
        [SerializeField] private float attackRange;
        [SerializeField] private GameObject foxesHome;
        private AIMovement _aiMovement;

        public FoxSensor _foxSensor { get; private set; }
        public float AttackRange => attackRange;
        private NavMeshAgent _foxEntity;
        [SerializeField] private float maxDistanceFromHome; 


        private void Awake()
        {
            _behaviorTree = new FoxBehaviorTree(this);
            _foxSensor = GetComponentInChildren<FoxSensor>();
            _foxEntity = GetComponent<NavMeshAgent>();
            _aiMovement = GetComponent<AIMovement>();
        }

        private void Start()
        {
            foxesHome = Instantiate(foxesHome, transform.position, Quaternion.identity);
        }

        void Update()
        {
            _behaviorTree.Update();
        }

        public void Wander()
        {
            // Use the NavMesh.SamplePosition function to find a valid point on the navmesh that is closest to the input point
            NavMeshHit hit;
            if (NavMesh.SamplePosition(foxesHome.transform.position, out hit, maxDistanceFromHome, NavMesh.AllAreas))
            {
                GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Sphere.GetComponent<Renderer>();
                Sphere.transform.position = hit.position;

                // Set the destination for the navmesh agent to the random point on the navmesh
                _aiMovement.SetDestination(hit.position);
            }

            // If the fox has left the home bounds, set the destination to the foxes home
            if (Vector3.Distance(transform.position, foxesHome.transform.position) > maxDistanceFromHome)
            {
                _aiMovement.SetDestination(foxesHome.transform.position);
            }
        }
    }
}

