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

        public void MoveTowards(Vector3 targetPosition)
        {
            _foxEntity.SetDestination(targetPosition);
        }

        public void Attack(GameObject rabbit)
        {
            // Set the destination of the NavMeshAgent to the position of the rabbit
            _foxEntity.SetDestination(rabbit.transform.position);

            // Check if the fox has reached its destination
            if (_foxEntity.remainingDistance < 0.1f)
            {
                // If the fox has reached its destination, destroy the rabbit game object
                Destroy(rabbit);
            }
        }

    }

