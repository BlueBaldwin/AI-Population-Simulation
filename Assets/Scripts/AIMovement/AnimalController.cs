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
        public AIMovement Movement;
        public AIBrain AIBrain { get; set; }
        public Stats Stats { get; set; }
        public STATE CurrentState { get; set; }
        
        void Start()
        {
            Movement = GetComponent<AIMovement>();
            AIBrain = GetComponent<AIBrain>();
        }

        private void Update()
        {
            // if (aiBrain.doneDeciding)
            // {
            //     aiBrain.doneDeciding = false;
            //     aiBrain.bestAction.Execute(this);
            // }
            FSMTick();

        }

        // Simple finate state machine to control the movement of Animals
        private void FSMTick()
        {
            if (CurrentState == STATE.MOVE)
            {
                AIBrain.DecideBestAction();
                // If we are at our desired position then change state
                if (Vector3.Distance(AIBrain.bestAction.RequiredDestination.position, transform.position) < 2f)
                {
                    CurrentState = STATE.EXECUTE;
                }
                else
                {
                    CurrentState = STATE.MOVE;
                }
            }
            else if(CurrentState == STATE.DECIDE)
            {
                if ((Vector3.Distance(AIBrain.bestAction.RequiredDestination.position, transform.position) < 2f))
                {
                    CurrentState = STATE.EXECUTE;
                }
                else
                {
                    Movement.SetDestination(AIBrain.bestAction.RequiredDestination.position);
                }
            }
            else if (CurrentState == STATE.EXECUTE)
            {
                if (!AIBrain.isBestActionFinished)
                {
                    AIBrain.bestAction.Execute(this);
                }
                else if(AIBrain.isBestActionFinished)
                {
                    CurrentState = STATE.DECIDE;
                }
            }
        }

        public void OnFinishedAction()
        {
            AIBrain.FindBestAction(actions);
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
            Stats.hunger -= 30;
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

            Stats.energy += 1;
            Debug.Log(name + "Sleep added 1 energy");
            OnFinishedAction();
        }
        #endregion
    }
}
