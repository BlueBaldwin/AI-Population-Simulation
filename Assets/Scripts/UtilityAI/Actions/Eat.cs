using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class Eat : Action
{
    public override void Execute(Controller animal)
    {
        animal.Forage(3);
    }
}
