using System.Collections;
using UnityEngine;

namespace AiMovement
{
    public enum STATE
    {
        DECIDE,
        MOVE,
        EXECUTE
    }
    public class AnimalController : MonoBehaviour
    {
        public GameObject aTransform;
        public AIMovement movement;
        public AIBrain aIBrain { get; set; }
        public Stats baseStats;
        public Stats stats;
        public MapContext mapContext;
        public STATE CurrentState { get; set; }
        private HUD hud;
        
        void Awake()
        {
            movement = GetComponent<AIMovement>();
            aIBrain = GetComponent<AIBrain>();
           // mapContext = GetComponent<MapContext>();
            CurrentState = STATE.DECIDE;
            hud = GetComponentInChildren<HUD>();
            stats = baseStats.CreateCopy();
        }
        private void Update()
        {
            FSMTick();
            //Debug.Log("Energy = " + stats.energy + "Hunger = " + stats.hunger);
            
        }

        // Simple finate state machine to control the movement of Animals
        private void FSMTick()
        {
            if (CurrentState == STATE.DECIDE)
            {
                aIBrain.FindBestAction();
                // If we are at our desired position then change state
                if (Vector3.Distance(aIBrain.bestAction.RequiredDestination.position, transform.position) < 2f)
                {
                    CurrentState = STATE.EXECUTE;
                }
                else
                {
                    CurrentState = STATE.MOVE;
                }
               
            }
            else if(CurrentState == STATE.MOVE)
            {
                if ((Vector3.Distance(aIBrain.bestAction.RequiredDestination.position, transform.position) < 2f))
                {
                    CurrentState = STATE.EXECUTE;
                }
                else
                {
                    movement.SetDestination(aIBrain.bestAction.RequiredDestination.position);
                }
            }
            else if (CurrentState == STATE.EXECUTE)
            {
                if (!aIBrain.isBestActionFinished)
                {
                    aIBrain.bestAction.Execute(this);
                }
                else if(aIBrain.isBestActionFinished)
                {
                    CurrentState = STATE.DECIDE;
                }
            }
        }

        public void OnFinishedAction()
        {
            aIBrain.FindBestAction();
        }

        #region Coroutines
        // FORAGE
        public void Forage(int time)
        {
            StartCoroutine(ForageCoroutine(time));
        }

        IEnumerator ForageCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(5);
                counter--;    
            }
            stats.hunger -= 30;
            Debug.Log(name + "Hunger decreased by 30");
            OnFinishedAction();
        }
    
        // SLEEP
        public void Sleep(int time)
        {
            StartCoroutine(SleepCoroutine(time));
        }

        IEnumerator SleepCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;    
            }

            stats.energy += 1;
            Debug.Log(name + "Sleep added 1 energy");
            OnFinishedAction();
        }
        #endregion
    }
}
