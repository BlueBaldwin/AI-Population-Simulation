using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AiMovement;

[CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
public class Sleep : Action
{
    public override void Execute(Controller animal)
    {
        animal.Sleep(3);
    }
}
