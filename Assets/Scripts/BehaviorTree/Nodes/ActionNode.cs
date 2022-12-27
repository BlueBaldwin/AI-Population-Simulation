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
			// Call the action delegate and return its result
			return _action();
		}

		public override bool CheckPreconditions()
		{
			
		}
	}
}
