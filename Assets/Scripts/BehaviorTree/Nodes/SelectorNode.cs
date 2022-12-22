using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SelectorNode : BehaviorTreeNode
    {
        public override BehaviorTreeStatus Execute()
        {
            // Iterate through all children nodes
            foreach (BehaviorTreeNode child in children)
            {
                // Check the preconditions for the child node
                if (child.CheckPreconditions())
                {
                    // If the preconditions are met, execute the child node
                    BehaviorTreeStatus childStatus = child.Execute();

                    // If the child node returns a success status, return a success status
                    if (childStatus == BehaviorTreeStatus.SUCCESS)
                    {
                        return BehaviorTreeStatus.SUCCESS;
                    }
                }
            }

            // If none of the children returned a success status, return a failure status
            return BehaviorTreeStatus.FAILURE;
        }


        public override bool CheckPreconditions()
        {
            // No preconditions for a SelectorNode
            return true;
        }
    }
}

