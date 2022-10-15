using System.Collections;
using UnityEngine;

namespace AiMovement
{
    public class Controller : MonoBehaviour
    {
        public AIMovement Movement;
        public AIBrain aiBrain { get; set; }
        public Action[] actions;
    
        void Start()
        {
            Movement = GetComponent<AIMovement>();
            aiBrain = GetComponent<AIBrain>();
        }

        private void Update()
        {
            if (aiBrain.doneDeciding)
            {
                aiBrain.doneDeciding = false;
                aiBrain.bestAction.Execute(this);
            }
        }

        private void OnFinishedAction()
        {
            aiBrain.FindBestAction(actions);
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
                yield return new WaitForSeconds(1);
                counter--;    
            }
            Debug.Log("Forage Timer complete");
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
            Debug.Log("Sleep Timer complete");
            OnFinishedAction();
        }
        #endregion
    }
}
