using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Define the base class for every action
public abstract class Action : ScriptableObject
{
    public string name;
    private float _score;

    public float Score
    {
        get { return _score; }
        set { _score = Mathf.Clamp01(value); } // Normalise the score bewteen 0-1
    }

    public Consideration[] Considerations;

    public void Awake()
    {
        Score = 0;
    }

    // Dependancy injection - we don't have a referenced Controller in this class, but thats ok because whoever calls this will just need to pass it through
    public abstract void Execute(Controller animal);
}