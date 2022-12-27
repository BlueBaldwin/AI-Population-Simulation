using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorTree
    {
        protected BehaviorTreeNode _rootNode;
        private List<BehaviorTreeNode> _nodes;
    
        public BehaviorTree()
        {
            _nodes = new List<BehaviorTreeNode>();
        }

        public void SetRootNode(BehaviorTreeNode rootNode)
        {
            _rootNode = rootNode;
        }
    
        public void AddNode(BehaviorTreeNode node)
        {
            _nodes.Add(node);
        }
    
        public virtual void Update()
        {
            foreach (BehaviorTreeNode node in _nodes)
            {
                node.Update();
            }
        }
    }
}

