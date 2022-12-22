using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class ConditionNode : BehaviorTreeNode
    {
        // A function that returns a boolean value indicating whether the condition is met or not
        public delegate bool ConditionDelegate();

        private ConditionDelegate _condition;

        public ConditionNode(ConditionDelegate condition)
        {
            _condition = condition;
        }

        public override BehaviorTreeStatus Execute()
        {
            return _condition() ? BehaviorTreeStatus.SUCCESS : BehaviorTreeStatus.FAILURE;

        }

        public override bool CheckPreconditions()
        {
            return true;
        }
    }
}
