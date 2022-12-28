using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;

public class GoToSleep : ActionNode
{
	private FoxController _foxController;

	public GoToSleep(FoxController foxController, FoxSensor sensor) : base(foxController, sensor)
	{
		_foxController = foxController;
	}

	public override BehaviorTreeStatus Update()
	{
		// Check if the fox is tired
		if (_foxController.IsTired())
		{
			Debug.Log("Fox is Tired");
			// If the fox is tired, try to go to sleep
			if (_foxController.GoToBed())
			{
				Debug.Log("Fox is In Bed");
				// If the fox successfully goes to sleep, return SUCCESS
				return BehaviorTreeStatus.SUCCESS;
			}
			else
			{
				// If the fox is still trying to go to sleep, return RUNNING
				Debug.Log("Fox is Going to bed");
				return BehaviorTreeStatus.RUNNING;
			}
		}
		else
		{
			Debug.Log("Fox is Not Tired");
			// If the fox is not tired, return FAILURE
			return BehaviorTreeStatus.FAILURE;
		}
	}
}
