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

    public abstract void Execute();
}
