using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAi;

public class RootSelectorNode : SelectorNode
{
    public RootSelectorNode(FoxController foxController, FoxSensor Sensor, AIMovement aiMovement) : base( foxController, sensor)
    {
        FoxController _foxController = foxController;
        FoxSensor sensor = Sensor;
        AIMovement _aiMovement = aiMovement;

        // Add the "Search for Prey" sequence node as a child
        SequenceNode searchForPreySequence = new SequenceNode();
        searchForPreySequence.AddChild(new IsHungry(_foxController));
        searchForPreySequence.AddChild(new SearchForPrey(_foxController, sensor, _aiMovement));
        searchForPreySequence.AddChild(new AttackRabbit(_foxController, sensor));
        AddChild(searchForPreySequence);

        // Add the "Reproduce" sequence node as a child
        SequenceNode reproduceSequence = new SequenceNode();
        reproduceSequence.AddChild(new IsHungry(_foxController));
        reproduceSequence.AddChild(new IsTired(_foxController));
        //reproduceSequence.AddChild(new Reproduce(_foxController));
        AddChild(reproduceSequence);

        // Add the "Go to Sleep" action node as a child
        AddChild(new GoToSleep(_foxController, sensor));
    }
}

