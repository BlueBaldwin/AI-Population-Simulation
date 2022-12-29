using System.Collections.Generic;
using UtilityAi;

public class SelectorNode : Node
{
	private List<Node> _children;
	private int _currentChildIndex;
	private FoxController _foxController;
	private FoxSensor _sensor;

	public SelectorNode(FoxController foxController, FoxSensor sensor)
	{
		_children = new List<Node>();
		_currentChildIndex = 0;
		_foxController = foxController;
		_sensor = sensor;
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