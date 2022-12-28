using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UtilityAi;
using Random = UnityEngine.Random;

public class FoxController : MonoBehaviour
    {
        // private FoxBehaviorTree _foxBehaviorTree;
        [SerializeField] private float attackRange;
        [SerializeField] private GameObject foxesHome;
        [SerializeField] private float energyDecreaseRate = 5; 
        
        private float hungerLevel;
        private float energyLevel;
       
        public FoxSensor FoxSensor { get; private set; }
        public float AttackRange => attackRange;
        private NavMeshAgent _foxEntity;
        [SerializeField] private float maxDistanceFromHome; 

        private BehaviorTree _behaviorTree;
        private AIMovement _aiMovement;
        private FoxSensor _foxSensor;
        
        private void Awake()
        {
            _foxSensor = GetComponentInChildren<FoxSensor>();
            _foxEntity = GetComponent<NavMeshAgent>();
            _aiMovement = GetComponent<AIMovement>();
            // Create a new behavior tree and set the root node
            _behaviorTree = new BehaviorTree(new RootSelectorNode(this, _foxSensor));
            StartCoroutine(DecreaseEnergyAndHunger());
        }

        private void Start()
        {
            foxesHome = Instantiate(foxesHome, transform.position, Quaternion.identity);
        }

        void Update()
        {
            _behaviorTree.Update();
        }
        
        // ACTIONS

        public void Wander()
        {
            if (!_foxEntity.hasPath && _foxEntity.remainingDistance < 0.5)
            {
                // To be implemented later for moving within the forward direction only until out of home bounds
                Vector3 currentDirection = transform.forward;

                // Generate a random point within the unit sphere centered at the origin
                Vector3 wanderPoint = Random.insideUnitSphere * maxDistanceFromHome;

                // Use the NavMesh.SamplePosition function to find a valid point on the navmesh that is closest to the input point
                NavMeshHit hit;
                if (NavMesh.SamplePosition(wanderPoint, out hit, maxDistanceFromHome, NavMesh.AllAreas))
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = hit.position;

                    // Set the destination for the navmesh agent to the random point on the navmesh
                    _aiMovement.SetDestination(hit.position);
                }

                // If the rabbit has left the home bounds, set the destination to the rabbit home
                if (Vector3.Distance(transform.position, foxesHome.transform.position) > maxDistanceFromHome)
                {
                    _aiMovement.SetDestination(foxesHome.transform.position);
                }
            }
        }

        public void MoveTowardsScent(Vector3 dropping)
        {
            if (dropping != null)
            {
                 Debug.Log("Moving to rabbit droppings");
                 _aiMovement.SetDestination(dropping);
            }
        }

        public bool GoToBed()
        {
            _aiMovement.SetDestination(foxesHome.transform.position);
            if (_foxEntity.remainingDistance <= 0.1)
            {
                return true;
            }
            return false;
        }
        
        public void Eat()
        {
            // Decrease the fox's hunger level by a certain amount
            hungerLevel -= 25;
            // Clamp the hunger level to a range of 0 to 100
            hungerLevel = Mathf.Clamp(hungerLevel, 0, 100);
        }

        // CONDITIONS
        
        public bool IsTired()
        {
            // Return true if the fox's energy level is low, false otherwise
            return false;
        }

        public bool IsHungry()
        {
            return true;
        }
        
        // COROUTINES
        private IEnumerator DecreaseEnergyAndHunger()
        {
            while (true)
            {
                yield return new WaitForSeconds(energyDecreaseRate);
                energyLevel--;
                hungerLevel++;
                // Clamp the energy and hunger levels to a maximum and minimum value
                energyLevel = Mathf.Clamp(energyLevel, 0, 100);
                hungerLevel = Mathf.Clamp(hungerLevel, 0, 100);
            }
        }
    }

