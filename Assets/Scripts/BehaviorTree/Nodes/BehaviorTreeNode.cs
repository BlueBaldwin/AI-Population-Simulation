using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum BehaviorTreeStatus
    {
        SUCCESS,
        FAILURE,
        RUNNING,
        TEST
    }

    public abstract class BehaviorTreeNode
    {
        protected List<BehaviorTreeNode> children;
    
        public BehaviorTreeNode()
        {
            children = new List<BehaviorTreeNode>();
        }
    
        public void AddChild(BehaviorTreeNode child)
        {
            children.Add(child);
        }

        public BehaviorTreeStatus Update()
        {
            return BehaviorTreeStatus.FAILURE;
        }
    
        public abstract BehaviorTreeStatus Execute();
        public abstract bool CheckPreconditions();
    }
}

