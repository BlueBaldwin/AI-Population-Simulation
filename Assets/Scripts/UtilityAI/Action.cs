using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;

//Define the base class for every action
public abstract class Action : ScriptableObject
{ 
    // Used to return the current best action for Debugging purposes
    public Consideration[] Considerations;
    public Transform RequiredDestination { get; protected set; }
    private float _score;

    public float Score
    {
        get { return _score; }
        set { _score = Mathf.Clamp01(value); } // Normalise the score bewteen 0-1
    }
 
    public void Awake()
    {
        Score = 0;
    }

    // Dependancy injection - we don't have a referenced Controller in this class, but thats ok because whoever calls this will just need to pass it through
    public abstract void Execute(AnimalController animal);
    public abstract void SetRequiredDestination(AnimalController animal);
}
