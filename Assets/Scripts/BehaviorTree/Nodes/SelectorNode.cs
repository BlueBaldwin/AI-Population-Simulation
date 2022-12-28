using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    private List<Node> _children;
    private int _currentChildIndex;

    public SelectorNode()
    {
        _children = new List<Node>();
        _currentChildIndex = 0;
    }

    public void AddChild(Node child)
    {
        _children.Add(child);
    }

    public override BehaviorTreeStatus Update()
    {
        while (_currentChildIndex < _children.Count)
        {
            Node currentChild = _children[_currentChildIndex];
            BehaviorTreeStatus status = currentChild.Update();

            if (status == BehaviorTreeStatus.SUCCESS)
            {
                _currentChildIndex = 0;
                return BehaviorTreeStatus.SUCCESS;
            }
            else if (status == BehaviorTreeStatus.RUNNING)
            {
                return BehaviorTreeStatus.RUNNING;
            }

            _currentChildIndex++;
        }

        _currentChildIndex = 0;
        return BehaviorTreeStatus.FAILURE;
    }
}

