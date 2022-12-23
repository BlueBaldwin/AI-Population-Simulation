using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;
using TreeEditor;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class Eat : Action
{

    public override void Execute(RabbitController rabbit)
    {
        rabbit.Forage();
    }

    public override void SetRequiredDestination(RabbitController rabbit)
    {
        
    }
}
