using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
	public class ActionNode : BehaviorTreeNode
	{
		// A function that performs a specific action and returns a `BehaviorTreeStatus` indicating whether the action was successful or not
		public delegate BehaviorTreeStatus ActionDelegate();

		private ActionDelegate _action;

		public ActionNode(ActionDelegate action)
		{
			_action = action;
		}
		
		public override BehaviorTreeStatus Execute()
		{
			// the code that performs a specific action, such as moving the fox towards its prey or attacking the prey.
			return BehaviorTreeStatus.TEST;
		}

		public override bool CheckPreconditions()
		{
			throw new System.NotImplementedException();
		}
	}
}
