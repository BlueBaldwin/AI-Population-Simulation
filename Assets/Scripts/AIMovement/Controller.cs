using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
        }
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
    }
    #endregion
}
