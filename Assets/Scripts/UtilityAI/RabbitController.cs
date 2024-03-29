using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace UtilityAi
{
    //Enum to control the State machine
    public enum STATE
    {
        DECIDE,
        MOVE,
    }
    
    public class RabbitController : MonoBehaviour
    {
        // Rate at which the rabbit's stats changes over time
        [SerializeField] private int energyDepletionRate;
        [SerializeField] private int hungerDepletionRate;
        [SerializeField] private int sleepRepletionRate;
        [SerializeField] private int hungerRepletionRate;
        // Times to complete actions
        [SerializeField] private int sleepTime;
        [SerializeField] private int eatTime;
        [SerializeField] float droppingBounds;
        
        [SerializeField] private float maxDistanceFromHome = 20.0f;
        [SerializeField] private GameObject rabbitDroppingPrefab;
        [SerializeField] float rabbitDroppingSpawnRadius = 1.0f;
        [SerializeField] float rabbitDroppingSpawnChance = 0.1f;

        private bool bFoundFood;
        private bool bIsAtHome;
        private bool bJustEaten;

        private GameObject foodObject;

        private HUD _hud;

        private STATE CurrentState { get; set; }
        private AIBrain AIBrain { get; set; }
        
        public NavMeshAgent agent;
        public AIMovement movement;
        public AISensor sensor;

        public GameObject rabbitsHome;

        public Stats stats;
    

        private void Awake()
        {
            rabbitsHome = Instantiate(rabbitsHome, transform.position, Quaternion.identity);
            CurrentState = STATE.DECIDE;
            stats = new Stats(100,100);
            
            AIBrain = GetComponent<AIBrain>();
            _hud = GetComponentInChildren<HUD>();
            movement = GetComponent<AIMovement>();
            sensor = GetComponentInChildren<AISensor>();
            agent = GetComponent<NavMeshAgent>();
            bJustEaten = false;
        }

        private void Start()
        {
            InvokeRepeating("EnergyDepleater", 10.0f, 10.0f);
        }

        private void Update()
        {
            FSMTick();
            _hud.UpdateStatsText(stats);
            if(bJustEaten) { SpawnRabbitDroppings(); }
        }
        
        private void FSMTick()
        {
            // If the current state is DECIDE, find the best action and execute it
            if (CurrentState == STATE.DECIDE)
            {
                AIBrain.FindBestAction();
                // If we are within distance of our destination, execute the best action and change the state back to DECIDE
                if (Vector3.Distance(movement.GetDestination(), transform.position) < 2f)
                {
                    AIBrain.bestAction.Execute(this);
                    CurrentState = STATE.DECIDE;
                }
                // If we are not at our destination, change the state to MOVE
                else
                {
                    CurrentState = STATE.MOVE;
                }
            }
            // Check if we are at our destination and execute the best action if we are
            else if(CurrentState == STATE.MOVE)
            {
                if ((Vector3.Distance(movement.GetDestination(), transform.position) < 2f))
                {
                    AIBrain.bestAction.Execute(this);
                    CurrentState = STATE.DECIDE;
                }
                else
                {
                    // If we are not at our destination, continue moving towards it
                    movement.SetDestination(movement.GetDestination());
                }
            }
        }

        // Changes state to Decide when current action is complete
        private void OnFinishedAction()
        {
            bFoundFood = false;
            CurrentState = STATE.DECIDE;
        }

        // Reduces the Rabbits stats continuously
        private void EnergyDepleater()
        {
            stats.DecreaseEnergy(energyDepletionRate);
            stats.IncreaseHunger(hungerDepletionRate);
            Debug.Log(this.name + "Energy is being removed" );
        }

        // Set the Rabbit to sample positions to search for food
        public void Forage()
        {
            // Check if the rabbit has found food in its current movement
            if (bFoundFood)
            {
                // If the rabbit has found food, move to it
                MoveToFood(foodObject);
            }
            else
            {
                // If the rabbit has not found food, sample a new position and set it as the destination
                if (!agent.hasPath && agent.remainingDistance < 0.5 && CurrentState != STATE.MOVE)
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
                        movement.SetDestination(hit.position);
                    }

                    // If the rabbit has left the home bounds, set the destination to the rabbit home
                    if (Vector3.Distance(transform.position, rabbitsHome.transform.position) > maxDistanceFromHome)
                    {
                        movement.SetDestination(rabbitsHome.transform.position);
                    }
                }
            }
        }

        // Set the destination to the rabbits home
        public void GoToBed()
        {
            // Set the destination to the home location
            movement.SetDestination(rabbitsHome.transform.position);

            // If we have reached the destination, start the sleep coroutine
            if (agent.remainingDistance < 0.1)
            {
                StartCoroutine(PerformAction("Sleep", sleepTime));
            }
        }
        
        // Found food bool true so set the destination to 
        private void MoveToFood(GameObject food)
        {
            movement.SetDestination(food.transform.position);
            if (agent.hasPath && agent.remainingDistance < 0.1)
            {
                StartCoroutine(PerformAction("Eat", eatTime));
                bFoundFood = false;
                StartCoroutine(ReactivateFoodObject(food, 20));
            } 
        }

        // Spawning rabbit poop mid way to next position
        private void SpawnRabbitDroppings()
        {
            if (Random.value < rabbitDroppingSpawnChance)
            {
                // Calculate the midpoint between the food position and the next destination
                Vector3 midpoint = Vector3.Lerp(foodObject.transform.position, movement.GetDestination(), 0.5f);
                if (Vector3.Distance(transform.position, midpoint) < rabbitDroppingSpawnRadius)
                {
                    // Instantiate a rabbit dropping at the midpoint
                    GameObject rabbitDropping = Instantiate(rabbitDroppingPrefab, midpoint, Quaternion.identity);
                    Vector3 d = gameObject.transform.forward;
                    rabbitDropping.GetComponent<RabbitDropping>().RabbitTrail.Set(d.x, d.y, d.z);
                    rabbitDropping.name = this.name + " Rabbit Dropping ";
                    bJustEaten = false; // set bJustEaten to false to stop spawning droppings
                }
            }
        }

        // Coroutine switch statement to perform time delays on the actions
        IEnumerator PerformAction(string action, int time)
        {
            agent.isStopped = true;
            switch (action)
            {
                case "Sleep":
                    
                    yield return new WaitForSeconds(time);
                    stats.IncreaseEnergy(sleepRepletionRate);
                    break;
                
                case "Eat":
                    
                    yield return new WaitForSeconds(time);
                    stats.DecreaseHunger(hungerRepletionRate);
                    bJustEaten = true;
                    SpawnRabbitDroppings();
                    break;
            }
            agent.isStopped = false;
            OnFinishedAction();
        }
        
        // Food consumption and regeneration 
        IEnumerator ReactivateFoodObject(GameObject food, float delay)
        {
            food.SetActive(false);
            yield return new WaitForSeconds(delay);
            food.SetActive(true);
        }
        
        // Public function to pass over any food found by the scanner
        public void SetFoodObject(GameObject food)
        {
            foodObject = food;
            bFoundFood = true;
        }
        
        // Draw a wireframe sphere around the home transform to show range
        void OnDrawGizmos()
        {
            // Home bounds
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(rabbitsHome.transform.position, maxDistanceFromHome);
        }
    }
}
