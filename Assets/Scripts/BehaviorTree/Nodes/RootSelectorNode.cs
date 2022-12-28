using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSelectorNode : SelectorNode
{
    public RootSelectorNode(FoxController foxController, FoxSensor sensor)
    {
        FoxController _foxController = foxController;
        FoxSensor Sensor = sensor;

        // Add the "Search for Prey" sequence node as a child
        SequenceNode searchForPreySequence = new SequenceNode();
        searchForPreySequence.AddChild(new IsHungry(_foxController));
        searchForPreySequence.AddChild(new SearchForPrey(_foxController, sensor));
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

