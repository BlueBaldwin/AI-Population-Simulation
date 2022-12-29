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
        [SerializeField] private float lowEnergy = 30; 
        [SerializeField] private int highHunger = 40; 
        [SerializeField] private int energyDecreaseRate = 5; 
        [SerializeField] private int energyIncreaseRate = 100; 
        [SerializeField] private int hungerDecreaseRate = 5; 
        [SerializeField] private int hungerIncreaseRate = 25; 
        
        private float hungerLevel;
        private float energyLevel;
       
        public FoxSensor FoxSensor { get; private set; }
        
        private NavMeshAgent _foxEntity;
        [SerializeField] private float maxDistanceFromHome; 

        private BehaviorTree _behaviorTree;
        public AIMovement _aiMovement;
        private FoxSensor _foxSensor;
        public FoxStats foxStats;
        
        private void Awake()
        {
            _foxSensor = GetComponentInChildren<FoxSensor>();
            _foxEntity = GetComponent<NavMeshAgent>();
            _aiMovement = GetComponent<AIMovement>();
            
            // Create a new behavior tree and set the root node
            _behaviorTree = new BehaviorTree(new RootSelectorNode(this, _foxSensor, _aiMovement));
            StartCoroutine(DecreaseEnergyAndHunger());

            foxStats = new FoxStats(100, 100, 10);
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
            if (_foxEntity.remainingDistance <= 0.2)
            {
                foxStats.Energy += energyIncreaseRate;
                return true;
            }
            return false;
        }
        
        public void Eat()
        {
            foxStats.Hunger -= hungerDecreaseRate;
        }

        // CONDITIONS
        
        public bool IsTired()
        {
            if (foxStats.Energy > lowEnergy)
            {
                return false;
            }
            return true;
        }

        public bool IsHungry()
        {
            if (foxStats.Hunger < highHunger)
            {
                return false;
            }

            return true;
        }
        
        // COROUTINES
        private IEnumerator DecreaseEnergyAndHunger()
        {
            while (true)
            {
                yield return new WaitForSeconds(energyDecreaseRate);
                foxStats.Energy -= energyDecreaseRate;
                foxStats.Hunger -= hungerIncreaseRate;
            }
        }

        public void MoveTowards(Vector3 targetPosition)
        {
            _foxEntity.SetDestination(targetPosition);
        }

        public void Attack(GameObject rabbit)
        {
            if (rabbit != null)
            {
                // Set the destination of the NavMeshAgent to the position of the rabbit
                _foxEntity.SetDestination(rabbit.transform.position);

                // Check if the fox has reached its destination
                if (_foxEntity.remainingDistance < 2f && _foxEntity.remainingDistance > 0.1)
                {
                    if (rabbit != null)
                    {
                        Destroy(rabbit);
                    }
                }
            }
        }

    }

