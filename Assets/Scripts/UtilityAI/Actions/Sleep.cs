using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
public class Sleep : Action
{
	public override void Execute(RabbitController rabbit)
	{
		rabbit.GoToBed();
	}

    public override void SetRequiredDestination(RabbitController rabbit)
    {
	   
    }
}
