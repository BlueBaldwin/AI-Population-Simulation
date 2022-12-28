using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviorTreeStatus
{
    SUCCESS,
    FAILURE,
    RUNNING,
}

public class BehaviorTree
{
    private Node _rootNode;

    public BehaviorTree(Node rootNode)
    {
        _rootNode = rootNode;
    }

    public void Update()
    {
        _rootNode.Update();
    }
}


