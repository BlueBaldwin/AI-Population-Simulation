using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;

[CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
public class Sleep : Action
{
    public override void Execute(AnimalController animal)
    {
        animal.Sleep(1);
    }

    public override void SetRequiredDestination(AnimalController animal)
    {
        
    }
}
