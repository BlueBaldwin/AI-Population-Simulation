using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxStats
{
    public float TirednessLevel { get; set; }
    public float HungerLevel { get; set; }
    public float ThirstLevel { get; set; }

    public FoxStats(float tiredness, float hunger, float thirst)
    {
        TirednessLevel = tiredness;
        HungerLevel = hunger;
        ThirstLevel = thirst;
    }
}

