using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class Eat : Action
{
    public override void Execute(AnimalController animal)
    {
        animal.Forage(1);
    }

    public override void SetRequiredDestination(AnimalController animal)
    {
        
    }
}
