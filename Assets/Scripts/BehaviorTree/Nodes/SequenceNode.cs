using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SequenceNode : BehaviorTreeNode
    {
        public override BehaviorTreeStatus Execute()
        {
            foreach (BehaviorTreeNode child in children)
            {
                if (child.Execute() == BehaviorTreeStatus.FAILURE)
                {
                    return BehaviorTreeStatus.FAILURE;
                }
            }
            return BehaviorTreeStatus.SUCCESS;
        }
    
        public override bool CheckPreconditions()
        {
            foreach (BehaviorTreeNode child in children)
            {
                if (!child.CheckPreconditions())
                {
                    return false;
                }
            }
            return true;
        }
    }
}

